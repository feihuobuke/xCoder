// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ParamParser.cs                                                                   
// *	Created @ 03/23/2012 8:05 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Specialized;
using System.Text;
using SF.xCoder.DB2Project.Extension;

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal class ParamParser : AbsParser
    {
        private readonly NameValueCollection @params = new NameValueCollection();

        public ParamParser(ParserOption option)
            : base(option, new ScopeTag {BeginTag = "<#=", CloseTag = "#>"})
        {
            Init();
            var data = Option.VariableParameter.Convert();
            @params.Add(data);
            foreach (string variable in Option.VariableCollection)
            {
                if (string.IsNullOrEmpty(data[variable]))
                {
                    @params.Add(variable, Option.VariableCollection[variable]);
                }
                else
                {
                    @params[variable] = Option.VariableCollection[variable];
                }
            }
        }

        private void Init()
        {
            OnParse += (ParamParser_OnParse);
        }

        private void ParamParser_OnParse(AbsParser parser, ParserEventArgs e)
        {
            var value = @params[e.Body.ToString().Trim(' ')];
            e.Replace = true;
            e.Replacement = new StringBuilder(value);
        }
    }
}