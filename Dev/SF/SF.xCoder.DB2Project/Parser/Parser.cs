// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/Parser.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Text;
using SF.xCoder.DB2Project.Parser.xCode;

namespace SF.xCoder.DB2Project.Parser
{
    internal class Parser
    {
        public Parser(ParserOption options)
        {
            Options = options;
        }

        public ParserOption Options { get; protected set; }

        public string Parse(ParserType type)
        {
            var tmp = string.Empty;
            switch (type)
            {
                case ParserType.XCODER:
                    var scope = new ScopeParser(Options);
                    scope.OnParse += (scope_OnParse);
                    var output = scope.Parse();
                    var param = new ParamParser(Options);
                    output = param.Parse(new StringBuilder(output));
                    tmp = output;
                    scope.Release();
                    param.Release();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            Options.Code = null;
            return tmp;
        }

        private void scope_OnParse(AbsParser parser, ParserEventArgs e)
        {
            var sc = e.Body.ToString();
            var option = e.Option.Clone() as ParserOption;
            var invoker = new ScopeInvoker(option, sc);
            invoker.Execute(e.Option.StatementParameters);
            e.Replace = true;
            e.Replacement = new StringBuilder(invoker.Result);
        }

        public void Release()
        {
            var gen = GC.GetGeneration(this);
            GC.Collect(gen);
        }
    }
}