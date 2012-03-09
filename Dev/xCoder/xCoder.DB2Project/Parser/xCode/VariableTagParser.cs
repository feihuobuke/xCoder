// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/VariableTagParser.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace xCoder.DB2Project.Parser.xCode
{
    internal class VariableTagParser : AbsParser
    {
        private const string ItemRegxFormat = "(.*)(={0})(.*)";

        public VariableTagParser(XCoderOptions options)
            : base(options, "<#(.[^#>]*[^<#]{0})#>") //@"\<\#(.[^(<#)]*[^(#>)])\#\>")
        {
        }


        public string Build(object parameter)
        {
            var data = parameter.Convert();
            foreach (string variable in Options.VariableCollection)
            {
                if (string.IsNullOrEmpty(data[variable]))
                {
                    data.Add(variable, Options.VariableCollection[variable]);
                }
            }
            Match[] results = Parse();
            string temp = TemplateContent;

            foreach (string item in data.Keys)
            {
                var regx = new Regex(string.Format(ItemRegxFormat, item), RegxOptions);
                List<Match> collection = results.Where(t => t.Success && regx.Match(t.Value).Success).ToList();
                string value = data[item];
                foreach (Match match in collection)
                {
                    Match subMatch = regx.Match(match.Groups[1].Value);
                    var words = new Regex(@"\w", RegxOptions);
                    string g1 = subMatch.Groups[1].Value;
                    string g2 = subMatch.Groups[2].Value;
                    string g3 = subMatch.Groups[3].Value;
                    if (words.Match(g1).Success || words.Match(g3).Success || string.IsNullOrEmpty(g2))
                    {
                        continue;
                    }
                    temp = temp.Replace(match.Value, value);
                }
            }

            return temp;
        }

        public void Render(NameValueCollection data, FileInfo file, bool append)
        {
            if (file.Exists && !append)
            {
                file.Delete();
            }
            string content = Build(data);
            File.WriteAllText(file.FullName, content);
        }
    }
}