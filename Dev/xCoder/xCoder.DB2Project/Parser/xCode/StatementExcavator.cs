// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/StatementRunner.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace xCoder.DB2Project.Parser.xCode
{
    internal class StatementExcavator : IDisposable
    {
        public const string EntryPoint = "CodeMethods";
        protected const string Invoker = "CodeInvoker";
        protected static Dictionary<Guid, CompilerResults> Cached;

        public StatementExcavator(ParserOption options, string sourceCode)
        {
            Options = options;
            SourceCode = sourceCode;
            if (Cached == null)
                Cached = new Dictionary<Guid, CompilerResults>();

        }

        public ParserOption Options { get; protected set; }
        public string Result { get; protected set; }
        public int ReturnCode { get; protected set; }
        public bool Successed { get; protected set; }

        public string SourceCode { get; protected set; }

        public event StatementErrorHandler Error;
        public event StatmentOutput Output;

        public void Execute(params object[] objects)
        {
            CompilerResults result = Compile();


            Successed = !result.Errors.HasErrors;
            if (Successed)
            {
                try
                {
                    Type type = result.CompiledAssembly.GetType(EntryPoint);
                    if (type != null)
                    {
                        object tmp = Activator.CreateInstance(type);
                        MethodInfo method = type.GetMethod(Invoker);
                        if (method != null)
                        {
                            object res = method.Invoke(tmp, objects);
                            Result = res as string;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Error != null)
                    {
                        Error(ex, SourceCode);
                    }
                }
            }
            result.TempFiles.Delete();
        }

        protected string GenerateMethod()
        {
            string tmp = "public string " + Invoker + " (DataBase DataBase,Table Table){ string Output=string.Empty;\r\n";
            tmp += ScParse();
            tmp += "\r\nreturn Output;}\r\n";
            return tmp;
        }

        const string ScopeRegx = @"\$(.[^($)]*[^(\$)])\$";
        const string OutputScopeRegx = @"#(.[^#]*[\ ,])";

        protected string ScParse()
        {
            var tmp = SourceCode;

            var regx = new Regex(ScopeRegx, RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var subRegx = new Regex(OutputScopeRegx, RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var matches = regx.Matches(SourceCode);
            foreach (var match in matches.OfType<Match>())
            {
                var itemTemp = match.Groups[1].Value;
                while (subRegx.IsMatch(itemTemp))
                {
                    var subMatches = subRegx.Matches(itemTemp);
                    foreach (var subMatch in subMatches.OfType<Match>())
                    {
                        itemTemp = itemTemp.Replace(subMatch.Value, string.Format("\"+{0}+@\" ", subMatch.Groups[1].Value));
                    }
                }

                tmp = tmp.Replace(match.Value, "Output+=@\"" + itemTemp + "\";");
            }
            return tmp;
        }

        protected string GenerateType()
        {

            var temp = new StringBuilder("using System;\r\n");
            foreach (string namesapce in Options.Namesapces)
            {
                if (!string.IsNullOrEmpty(namesapce))
                {
                    temp.AppendLine(string.Format("using {0};", namesapce));
                }

            }
            temp.AppendLine("public class " + EntryPoint + " {");
            temp.AppendLine(GenerateMethod());
            temp.AppendLine("}");
            return temp.ToString();
        }

        protected CompilerResults Compile()
        {
            CompilerResults result;
            if (Cached.Keys.Contains(Options.InstanceId))
            {
                result = Cached[Options.InstanceId];
            }
            else
            {
                string source = GenerateType();
                var options = new CompilerParameters
                                  {
                                      CompilerOptions = "/target:library /optimize",
                                      GenerateExecutable = true,
                                      GenerateInMemory = true,
                                      IncludeDebugInformation = false,
                                  };
                options.ReferencedAssemblies.AddRange(Options.References.OfType<string>().ToArray());
                result = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(options, source);
                ReturnCode = result.NativeCompilerReturnValue;
                foreach (string item in result.Output)
                {
                    if (Output != null)
                    {
                        Output(item);
                    }
                }
                if (result.Errors.HasErrors)
                {
                    foreach (CompilerError error in result.Errors)
                    {
                        var message = string.Format("({0}) : {1} @({2},{3})", error.ErrorNumber, error.ErrorText,
                                                    error.Line,
                                                    error.Column);
                        Console.WriteLine(message);
                        var ex = new Exception(message);
                        if (Error != null)
                        {
                            Error(ex, SourceCode);
                        }
                    }
                }
                Cached.Add(Options.InstanceId, result);
            }
            return result;
        }


        public void Release()
        {

        }

        public void Dispose()
        {

        }
    }
}