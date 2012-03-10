// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.Test/xCoderPaser.cs                                                                   
// *	Created @ 03/05/2012 3:43 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xCoder.DB2Project.Data;
using xCoder.DB2Project.Data.Type;
using xCoder.DB2Project.Parser;
using xCoder.DB2Project.Parser.xCode;

namespace xCoder.Test.ParserTest
{
    [TestClass]
    public class xCoderPaser
    {
        [TestMethod]
        public void ParserTest()
        {
            var builder = new DBBuilder(new DBConnection
                                            {
                                                Account = "gonnatour",
                                                DBType = DataBaseType.MSSQL,
                                                Name = "gonnatour",
                                                Password = "passw0rd",
                                                Server = "localhost"
                                            });
            DataBase database = builder.Build();
            var options = new ParserOption();

            foreach (Table table in database.Tables)
            {
                options.VariableParameter = table;
                table.Columns = table.Columns.OrderByDescending(t => t.PrimaryKey).ThenBy(t => t.Name).ToList();
                options.StatementParameters = new object[] { database, table };
                options.Namesapces.Add("xCoder.DB2Project.Data");
                options.References.Add("System.dll");
                options.References.Add(@".\xCoder.DB2Project.dll");
                options.SourceCode = new FileInfo("./testdata/test2.tpl");
                var parser = new Parser(options);
                string temp = parser.Parse(ParserType.XCODER);
                Console.WriteLine(temp);
            }
        }
    }
}