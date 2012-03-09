using System;
using System.IO;
using xCoder.Parser.xCode;

namespace xCoder.Parser
{
    public class Parser
    {
        public XCoderOptions Options { get; protected set; }

        public Parser(XCoderOptions options)
        {
            Options = options;
        }

        
        public string Parse(ParserType type)
        {
            var tmp = string.Empty;
            switch (type)
            {
                case ParserType.XCODER:
                    var statement = new StatementParser(Options);
                    var temp = statement.Build(Options.StatementParameters);
                    Options.Code = new StringReader(temp);
                    var varies = new VariableTagParser(Options);
                    tmp = varies.Build(Options.VariableParameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            Options.Code.Close();
            return tmp;
        }
    }
}