// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/SF.xCoder.DB2Project.ConsoleGenor/Program.cs                                                                   
// *	Created @ 03/23/2012 9:57 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************
using System;
using System.IO;
using SF.xCoder.DB2Project.Builder;
using SF.xCoder.DB2Project.Data;
using SF.xCoder.DB2Project.Data.Type;

namespace SF.xCoder.DB2Project.ConsoleGenor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DataBase db = BuildDB();
            var options = new BuilderOption
                              {
                                  Namespace = "Com.Vervidian.HireCredit.DataAccess",
                                  OutputDirectory = new DirectoryInfo(@"C:\project\HireCredit\Com.Vervidian.HireCredit.DataAccess\Dao"),
                                  DataBase = db,
                                  Template = new FileInfo("./testdata/ParserTest.tpl"),
                                  Override = true
                              };
            options.Namespace = "Com.Vervidian.HireCredit.Bean";
            options.OutputDirectory = new DirectoryInfo(@".\Bean");
            options.FileNameFormat = "{0}Bean";

            var facedeBuilder = new ClassBuilder(options);
            string[] files = facedeBuilder.Build();
            foreach (string file in files)
            {
                Console.WriteLine("file : " + file);
            }

            Console.ReadLine();
        }

        private static DataBase BuildDB()
        {
            var dbBuilder = new DBBuilder(new DBConnection
                                              {
                                                  DBType = DataBaseType.SQLCE,
                                                  Name = @"C:\project\HireCredit\Com.Vervidian.HireCredit.Web\App_Data\data.sdf",
                                                  Password = "Passw0rd"
                                              });
            DataBase db = dbBuilder.Build();
            return db;
        }
    }
}