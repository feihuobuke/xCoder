// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/StatementParser.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

namespace xCoder.DB2Project.Parser.xCode
{
    internal class StatementParser : ParserBase
    {
        public StatementParser(ParserOption options)
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
                var runner = new StatementExcavator(Options, statement);
                runner.Execute(parameters);
                var replacemenet = runner.Successed ? runner.Result : string.Empty;
                temp = temp.Replace(match.Value, replacemenet);
            }
            return temp;
        }
    }
}