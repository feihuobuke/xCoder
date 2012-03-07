using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xCoder.Parser.xCode;

namespace xCoder.Test.ParserTest
{
    [TestClass]
    public class xCoderPaser
    {
        [TestMethod]
        public void ParserTest()
        {
            //var parser = new xCodeParser();
            //parser.Parse(new FileInfo(@"D:\Personal\VS2010\xCoder\xCoder.Test\MsSqlTest\ReaderTest.cs"));
            var codeProvider = new CSharpCodeProvider{};

            var genor = codeProvider.CreateGenerator(@"C:\Users\CTI\Desktop\xxx.cs");
            //var parser = codeProvider.CreateGenerator();
            //var unit = parser.Parse(new StringReader("using System.IO;"));
            //using (var writer = new StreamWriter(@"C:\Users\CTI\Desktop\xxx.cs", true))
            //{
            //    codeProvider.GenerateCodeFromCompileUnit(unit, writer, new CodeGeneratorOptions());
            //    writer.Close();
            //}

        }
    }
}