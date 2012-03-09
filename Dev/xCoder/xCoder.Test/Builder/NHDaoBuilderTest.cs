// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.Test/NHDaoBuilderTest.cs                                                                   
// *	Created @ 03/09/2012 8:52 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xCoder.DB2Project.Builder;
using xCoder.DB2Project.Data;
using xCoder.DB2Project.Data.Type;

namespace xCoder.Test.Builder
{
    [TestClass]
    public class NHDaoBuilderTest
    {
        [TestMethod]
        public void BuildTest()
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
            var parameter = new BuilderParameters { DataBase = database, OutputDirectory = new DirectoryInfo("./../DAO/"), Template = new FileInfo("./testdata/test2.tpl"),Namespace = "xCode.Test.Objects"};
            var daoBuilder = new ClassBuilder(parameter);
            var files = daoBuilder.Build();
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            var hbmBuilder = new HBMBuilder (parameter);
            files = hbmBuilder.Build();
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}