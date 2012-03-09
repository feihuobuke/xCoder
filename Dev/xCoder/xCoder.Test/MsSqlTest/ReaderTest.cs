// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.Test/ReaderTest.cs                                                                   
// *	Created @ 02/27/2012 10:39 AM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xCoder.DB2Project.Builder;
using xCoder.DB2Project.Comm.Util;
using xCoder.DB2Project.Data;
using xCoder.DB2Project.Data.Type;

namespace xCoder.Test.MsSqlTest
{
    [TestClass]
    public class ReaderTest
    {
        [TestMethod]
        public void Read()
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
            Console.WriteLine(database.Tables.Count);

            foreach (Table table in database.Tables)
            {
                string json = JsonUtil.Convert(table);
                Console.WriteLine("TABLE : " + json);
                foreach (Column column in table.Columns)
                {
                    json = JsonUtil.Convert(column);
                    Console.WriteLine("     COLUMN : " + json);
                    foreach (ForeignKey foreignKey in column.ForeignKeys)
                    {
                        json = JsonUtil.Convert(foreignKey);
                        Console.WriteLine("         Foreign Key : " + json);
                    }
                }
            }

            var nhBuilder =
                new HBMBuilder(new BuilderParameters
                                          {
                                              DataBase = database,
                                              Namespace = "test",
                                              OutputDirectory =
                                                  new DirectoryInfo(@"D:\Personal\VS2010\xCoder\xCoder.Test\test\nh\")
                                          });
            string[] files = nhBuilder.Build();
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }

        [TestMethod]
        public void TypeRefTest()
        {
            var mappedType = TypeMap.GetTypeString("Binary", true);
            Console.WriteLine(mappedType);
            mappedType = TypeMap.GetTypeString("DateTime", true);
            Console.WriteLine(mappedType);
            mappedType = TypeMap.GetTypeString("Binary", false);
            Console.WriteLine(mappedType);
            mappedType = TypeMap.GetTypeString("DateTime", false);
            Console.WriteLine(mappedType);
        }
    }
}