// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/AbsParser.cs                                                                   
// *	Created @ 03/22/2012 7:10 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal abstract class AbsParser
    {
        protected AbsParser(ParserOption option, ScopeTag tag)
        {
            Option = option;
            Tag = tag;
            Code = Option.SourceCode.Exists
                       ? new StringBuilder(File.ReadAllText(Option.SourceCode.FullName, Encoding.UTF8))
                       : new StringBuilder("");
            Indexes = new List<ScopeIndex>();
            Results = new List<StringBuilder>();
        }

        public ParserOption Option { get; set; }
        protected List<ScopeIndex> Indexes { get; set; }
        protected List<StringBuilder> Results { get; set; }
        protected StringBuilder Code { get; set; }
        protected ScopeTag Tag { get; set; }

        protected virtual void ParseIndexes(StringBuilder code)
        {
            Indexes = new List<ScopeIndex>();
            Results = new List<StringBuilder>();

            var length = code.Length;
            var beginHolding = false;
            var beginHoldingIndex = -1;
            for (int i = 0; i < length; i++)
            {
                var tmp = code[i];
                //hit begin
                var beginHit = false;
                var beginTagLen = Tag.BeginTag.Length;
                for (int j = 0; j < beginTagLen; j++)
                {
                    beginHit = (code[i + j] == Tag.BeginTag[j]);
                    if (!beginHit)
                    {
                        break;
                    }
                }
                if (beginHit)
                {
                    Indexes.Add(new ScopeIndex(Tag) { StartIndex = i });
                    beginHolding = true;
                    beginHoldingIndex = i;
                    Console.WriteLine("Hit Begin : Index - " + i);
                    i += beginTagLen;

                    continue;
                }
                if (beginHolding)
                {
                    if (tmp == ' ' && Tag.BeginDropInNextSpace && beginHoldingIndex > -1)
                    {
                        int holdingIndex = beginHoldingIndex;
                        var index = Indexes.FirstOrDefault(t => t.StartIndex == holdingIndex && t.EndIndex < 0);
                        if (index != null)
                            Indexes.Remove(index);
                        beginHoldingIndex = -1;
                        beginHolding = false;
                    }
                }
                //hit end

                var closeHit = false;
                var closeTagLen = Tag.CloseTag.Length;
                for (int j = 0; j < closeTagLen; j++)
                {
                    closeHit = (code[i + j] == Tag.CloseTag[j]);
                    if (!closeHit)
                    {
                        break;
                    }
                }
                if (closeHit)
                {
                    var index = Indexes.OrderBy(t => t.StartIndex).Where(t => t.EndIndex == 0).AsQueryable().FirstOrDefault();
                    if (index != null)
                    {
                        var itemIndex = Indexes.IndexOf(index);
                        Console.WriteLine("Hit Close : Index - " + i);
                        Indexes[itemIndex].EndIndex = i + closeTagLen;
                        i += closeTagLen;
                    }
                }
            }
        }

        public virtual event ParserHandler OnParse;

        public string Parse(StringBuilder code)
        {
            Code = code;
            ParseIndexes(code);
            var tmp = new StringBuilder(code.ToString());
            var indexes = Indexes.Where(t => t.Length > 0);
            var tmpDic = new Dictionary<ScopeIndex, StringBuilder>();
            foreach (var index in indexes)
            {
                var str = Code.ToString(index);
                Console.WriteLine("SCOPE : " + index.ToString());
                Console.WriteLine(Code.ScopeBody(index));
                var result = new StringBuilder(str);
                Results.Add(result);
                tmpDic.Add(index, result);
            }
            if (OnParse != null)
            {
                foreach (var result in tmpDic)
                {
                    var str = Code.ToString(result.Key);
                    var body = new StringBuilder(result.Value.ToString());
                    body = body.Remove(0, Tag.BeginTag.Length);
                    body = body.Remove(body.Length - Tag.CloseTag.Length, Tag.CloseTag.Length);
                    var args = new ParserEventArgs
                                   {
                                       Replace = true,
                                       Replacement = new StringBuilder(),
                                       Result = result.Value,
                                       Tag = Tag,
                                       Index = result.Key,
                                       Body = body,
                                       Option = Option
                                   };
                    OnParse.Invoke(this, args);
                    if (args.Replace)
                    {
                        tmp = tmp.Replace(str, args.Replacement.ToString());
                    }
                }
            }
            return tmp.ToString();
        }

        public virtual string Parse()
        {
            return Parse(Code);
        }

        public void Release()
        {
            var gen = GC.GetGeneration(this);
            GC.Collect(gen);
        }
    }
}