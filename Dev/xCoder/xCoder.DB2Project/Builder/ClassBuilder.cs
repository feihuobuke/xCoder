// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/DAOClassBuilder.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.IO;
using System.Linq;
using xCoder.DB2Project.Data;
using xCoder.DB2Project.Parser;
using xCoder.DB2Project.Parser.xCode;

namespace xCoder.DB2Project.Builder
{
    public class ClassBuilder : AbsBuilder
    {
        public ClassBuilder(BuilderParameters parameters)
            : base(parameters)
        {
        }

        public override string[] Build()
        {
            var options = new XCoderOptions();
            var currentAssembly = GetType().Assembly;
            var name = new FileInfo(currentAssembly.Location).Name;
            var namespaces = currentAssembly.GetTypes().Select(t => t.Namespace).ToArray();
            options.Namesapces.AddRange(namespaces);
            options.References.Add("System.dll");
            options.References.Add(@".\" + name);
            options.SourceCode = Parameters.Template;
            options.VariableCollection.Add("Namespace",
                                           string.IsNullOrEmpty(Parameters.Namespace) ? "" : Parameters.Namespace);
            var tmp = new List<string>();
            foreach (Table table in Parameters.DataBase.Tables)
            {
                options.VariableParameter = table;
                options.StatementParameters = new object[] { Parameters.DataBase, table };

                var parser = new Parser.Parser(options);
                string temp = parser.Parse(ParserType.XCODER);
                string fileName = Path.Combine(Parameters.OutputDirectory.FullName, table.Name) + ".cs";
                File.WriteAllText(fileName, temp);
                tmp.Add(fileName);
            }
            return tmp.ToArray();
        }
    }
}