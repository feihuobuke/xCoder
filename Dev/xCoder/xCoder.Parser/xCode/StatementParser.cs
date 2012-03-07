using System.IO;

namespace xCoder.Parser.xCode
{
    internal class StatementParser : AbsParser
    {
        public StatementParser(XCoderOptions options)
            : base(options, "<%(.[^%>]*[^%>])%>")
        {
        }

        public string Build(params object[] parameters)
        {
            var temp = TemplateContent;
            var list = Parse();
            foreach (var match in list)
            {
                var statement = match.Groups[1].Value;
                var runner = new StatementRunner(Options, statement);
                runner.Execute(parameters);
                var replacemenet = runner.Successed ? runner.Result : string.Empty;
                temp = temp.Replace(match.Value, replacemenet);
            }
            return temp;
        }
    }
}