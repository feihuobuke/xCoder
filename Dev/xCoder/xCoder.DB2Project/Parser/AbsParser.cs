// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/AbsParser.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using xCoder.DB2Project.Parser.xCode;

namespace xCoder.DB2Project.Parser
{
    internal abstract class AbsParser
    {
        protected static readonly RegexOptions RegxOptions = RegexOptions.Singleline | RegexOptions.Multiline |
                                                             RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

        internal Regex BaseRegex;
        internal string TagRegx;

        protected AbsParser(XCoderOptions options, string tagRegx)
        {
            Options = options;
            TagRegx = tagRegx;
            BaseRegex = new Regex(TagRegx.Replace(@"\r", string.Empty).Replace("\n", string.Empty), RegxOptions);
            if ((Options.SourceCode == null || !Options.SourceCode.Exists) && Options.Code == null)
            {
                throw new InvalidDataException("XCoderOptions: Code File To be parsed can not be found");
            }
            TemplateContent = Options.Code != null
                                  ? Options.Code.ToString()
                                  : File.ReadAllText(options.SourceCode.FullName);
            Results = new List<string>();
        }

        public XCoderOptions Options { get; set; }
        protected string TemplateContent { get; set; }

        public List<string> Results { get; protected set; }

        internal virtual Match[] Parse()
        {
            var res = BaseRegex.Matches(TemplateContent).OfType<Match>().ToArray();
            Results.AddRange(res.Select(t => t.Value));
            return res;
        }
    }
}