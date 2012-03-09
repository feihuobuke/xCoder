// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/Parser.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.IO;
using System.Text;
using xCoder.DB2Project.Parser.xCode;

namespace xCoder.DB2Project.Parser
{
    public class Parser
    {
        public Parser(XCoderOptions options)
        {
            Options = options;
        }
        public XCoderOptions Options { get; protected set; }
        public string Parse(ParserType type)
        {
            var tmp = string.Empty;
            switch (type)
            {
                case ParserType.XCODER:
                    var statement = new StatementParser(Options);
                    var temp = statement.Build(Options.StatementParameters);
                    Options.Code = new StringBuilder(temp);
                    var varies = new VariableTagParser(Options);
                    tmp = varies.Build(Options.VariableParameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            Options.Code = null;
            return tmp;
        }
    }
}