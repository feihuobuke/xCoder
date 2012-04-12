// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/FileBuilder.cs                                                                   
// *	Created @ 03/10/2012 6:41 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using SF.xCoder.DB2Project.Data;
using SF.xCoder.DB2Project.Data.Type;
using SF.xCoder.DB2Project.Parser;

namespace SF.xCoder.DB2Project.Builder
{
    public class FileBuilder : AbsBuilder
    {
        public FileBuilder(BuilderOption parameters, string extension)
            : base(parameters)
        {
            Extension = extension;
        }

        public string Extension { get; set; }

        public override string[] Build()
        {
            var options = BuildOption();
            options.SourceCode = Parameters.Template;
            var tmp = new List<string>();
            var parser = new Parser.Parser(options);
            string fileName;
            string temp;
            string fName;
            DirectoryInfo dir;
            switch (Parameters.FileDependency)
            {
                case FileDepend.DATABASE:
                    parser.Options.VariableParameter = Parameters.DataBase;
                    parser.Options.StatementParameters = new object[] { Parameters.DataBase };
                    temp = parser.Parse(ParserType.XCODER);
                    fName = string.IsNullOrEmpty(Parameters.FileNameFormat)
                                ? Parameters.DataBase.Connection.Name
                                : string.Format(Parameters.FileNameFormat, Parameters.DataBase.Connection.Name);
                    fileName = Path.Combine(Parameters.OutputDirectory.FullName, fName + Extension);
                    dir = new FileInfo(fileName).Directory;
                    if (dir != null && !dir.Exists)
                    {
                        dir.Create();
                    }
                    if (Parameters.Override)
                    {
                        File.WriteAllText(fileName, temp);
                        tmp.Add(fileName);
                    }
                    else
                    {
                        if (!File.Exists(fileName))
                        {
                            File.WriteAllText(fileName, temp);
                        }
                        tmp.Add(fileName);
                    }

                    break;
                case FileDepend.TABLES:
                    foreach (Table table in Parameters.DataBase.Tables)
                    {
                        parser.Options.VariableParameter = table;
                        parser.Options.StatementParameters = new object[] { Parameters.DataBase, table };
                        temp = parser.Parse(ParserType.XCODER);
                        fName = string.IsNullOrEmpty(Parameters.FileNameFormat)
                                    ? table.Name
                                    : string.Format(Parameters.FileNameFormat, table.Name);
                        fileName = Path.Combine(Parameters.OutputDirectory.FullName, fName + Extension);
                        dir = new FileInfo(fileName).Directory;
                        if (dir != null && !dir.Exists)
                        {
                            dir.Create();
                        }
                        if (Parameters.Override)
                        {
                            File.WriteAllText(fileName, temp);
                            tmp.Add(fileName);
                        }
                        else
                        {
                            if (!File.Exists(fileName))
                            {
                                File.WriteAllText(fileName, temp);
                            }
                            tmp.Add(fileName);
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            parser.Release();
            return tmp.ToArray();
        }
    }
}