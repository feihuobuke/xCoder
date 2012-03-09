using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xCoder.Bean;
using xCoder.Logic;
using System.Web;
using xCoder.Logic.Builder;
using xCoder.Parser;
using xCoder.Parser.xCode;

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
            var database = builder.Build();
            Console.WriteLine(database.Tables.Count);

            foreach (var table in database.Tables)
            {
                var json = JsonUtil.Convert(table);
                Console.WriteLine("TABLE : " + json);
                foreach (var column in table.Columns)
                {
                    json = JsonUtil.Convert(column);
                    Console.WriteLine("     COLUMN : " + json);
                    foreach (var foreignKey in column.ForeignKeys)
                    {
                        json = JsonUtil.Convert(foreignKey);
                        Console.WriteLine("         Foreign Key : " + json);
                    }
                }
            }

            var nhBuilder = new NHibernateBuilder(new BuilderParameters { DataBase = database, Namespace = "test", OutputDirectory = new DirectoryInfo(@"D:\Personal\VS2010\xCoder\xCoder.Test\test\nh\") });
            var files = nhBuilder.Build();
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        [TestMethod]
        public void Vares()
        {
            //var temp = File.ReadAllText("./testdata/test2.tpl", Encoding.UTF8);
            var builder = new DBBuilder(new DBConnection
            {
                Account = "gonnatour",
                DBType = DataBaseType.MSSQL,
                Name = "gonnatour",
                Password = "passw0rd",
                Server = "localhost"
            });
            var database = builder.Build();
            var vars = database.Tables;
            var options = new XCoderOptions();

            foreach (var table in database.Tables)
            {
                options.VariableParameter = table;
                options.StatementParameters = new object[] { database, table };
                options.Namesapces.Add("xCoder.Bean");
                options.References.Add("System.dll");
                options.References.Add(@".\xCoder.Bean.dll");
                options.SourceCode = new FileInfo("./testdata/test2.tpl");
                var parser = new Parser.Parser(options);
                var temp = parser.Parse(ParserType.XCODER);
                Console.WriteLine(temp);
            }

            //var collection = new ValueCollection();
            //collection.Add("Name", vars.Name);
            //var template = new VariableTagParser(new StringReader(temp)).Build(collection);

            //Console.WriteLine(template);
            //var statement = new StatementParser(new StringReader(template));
            //statement.Build();
            //foreach (var result in statement.Results)
            //{
            //    Console.WriteLine(result);
            //}

        }

        [TestMethod]
        public void Runner()
        {
            //var runner = new StatementRunner("Output=\"<#=Name#>\";");
            //runner.Namesapces.Add("xCoder.Bean");
            //runner.Error += new StatementErrorHandler(runner_Error);
            //runner.Output += new StatmentOutput(runner_Output);
            //runner.References.Add("System.dll");
            //runner.References.Add(@"D:\Personal\VS2010\xCoder\xCoder.Bean\bin\Debug\xCoder.Bean.dll");
            //runner.Execute(new DataBase(), new Table());
            //Console.WriteLine(runner.Result);
        }

        void runner_Output(string output)
        {
            Console.WriteLine(output);
        }

        void runner_Error(Exception ex, string statment)
        {
            Console.WriteLine(ex.Message);
        }
    }
}