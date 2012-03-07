using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using xCoder.Parser.xCode;

namespace xCoder.Parser
{
    internal abstract class AbsParser
    {
        public XCoderOptions Options { get; set; }
        internal string TagRegx;
        internal Regex BaseRegex;
        protected string TemplateContent { get; set; }
        protected static readonly RegexOptions RegxOptions = RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

        protected AbsParser(XCoderOptions options, string tagRegx)
        {
            Options = options;
            TagRegx = tagRegx;
            BaseRegex = new Regex(TagRegx.Replace(@"\r", string.Empty).Replace("\n", string.Empty), RegxOptions);
            if ((Options.SourceCode == null || !Options.SourceCode.Exists) && Options.Code == null)
            {
                throw new InvalidDataException("XCoderOptions: Code File To be parsed can not be found");
            }
            TemplateContent = Options.Code != null ? Options.Code.ReadToEnd() : File.ReadAllText(options.SourceCode.FullName);
            Results = new List<string>();
        }
        internal virtual Match[] Parse()
        {
            var res = BaseRegex.Matches(TemplateContent).OfType<Match>().ToArray();
            Results.AddRange(res.Select(t => t.Value));
            return res;
        }
        public List<string> Results { get; protected set; }
    }
}