// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/AbsInvoker.cs                                                                   
// *	Created @ 03/23/2012 6:46 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal abstract class AbsInvoker
    {
        public const string EntryPoint = "CodeMethods";
        protected const string Invoker = "CodeInvoker";
        protected static Dictionary<Guid, CompilerResults> Cached;
        protected ScopeParser OutputParameterParser;
        protected ScopeParser OutputParser;

        protected AbsInvoker(ParserOption options, string sourceCode)
        {
            Options = options;
            SourceCode = sourceCode;
            OutputParser = new ScopeParser(options, new ScopeTag {BeginTag = "<$@", CloseTag = "@$>"});
            OutputParameterParser = new ScopeParser(options,
                                                    new ScopeTag
                                                        {BeginTag = "@(", CloseTag = ")", BeginDropInNextSpace = true});

            if (Cached == null)
                Cached = new Dictionary<Guid, CompilerResults>();
        }

        public ParserOption Options { get; protected set; }
        public string Result { get; protected set; }
        public int ReturnCode { get; protected set; }
        public bool Successed { get; protected set; }

        public string SourceCode { get; protected set; }

        public void Dispose()
        {
        }

        public event StatementErrorHandler Error;
        public event StatmentOutput Output;

        protected string GetOutputScope(string str)
        {
            var chars = new List<char>();
            var hit = false;
            chars.Add('@');
            chars.Add('"');
            foreach (char c in str)
            {
                if (c == '#')
                {
                    hit = true;
                    chars.Add('"');
                    chars.Add('+');
                    chars.Add('@');
                    continue;
                }
                if (hit && c == ' ')
                {
                    hit = false;
                    chars.Add('+');
                    chars.Add('@');
                    chars.Add('"');
                    continue;
                }
                if (c == '"')
                {
                    chars.Add('\'');
                    continue;
                }
                chars.Add(c);
            }
            chars.Add('"');
            var tmp = new string(chars.ToArray());
            return tmp; //string.Format("Output={0};", tmp);
        }

        public void Execute(params object[] objects)
        {
            CompilerResults result = Compile();

            Successed = !result.Errors.HasErrors;
            if (Successed)
            {
                try
                {
                    Type type =
                        result.CompiledAssembly.GetTypes().FirstOrDefault(
                            t =>
                            t != null && t.FullName != null &&
                            t.FullName.EndsWith(EntryPoint, StringComparison.CurrentCultureIgnoreCase));
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
        }

        protected string GenerateMethod()
        {
            string tmp = "public string " + Invoker +
                         " (DataBase DataBase,Table Table){ string Output=string.Empty;\r\n";
            tmp += ScParse();
            tmp += "\r\nreturn Output;}\r\n";
            return tmp;
        }

        protected string ScParse()
        {
            var tmp = SourceCode;
            OutputParser.OnParse += (OutputParser_OnParse);
            var statment = OutputParser.Parse(new StringBuilder(tmp));
            OutputParser.OnParse -= OutputParser_OnParse;
            return statment;
        }

        private void OutputParser_OnParse(AbsParser parser, ParserEventArgs e)
        {
            var tmp = e.Body;
            OutputParameterParser.OnParse += (OutputParameterParser_OnParse);
            var body = OutputParameterParser.Parse(tmp);
            OutputParameterParser.OnParse -= (OutputParameterParser_OnParse);
            e.Replace = true;
            e.Replacement = new StringBuilder(string.Format("Output += @\"{0}\";", body));
        }

        private void OutputParameterParser_OnParse(AbsParser parser, ParserEventArgs e)
        {
            var tmp = e.Body;
            e.Replace = true;
            e.Replacement = new StringBuilder(string.Format("\"+{0}+@\"", tmp));
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
            temp.AppendLine("namespace SF.xCoder.DB2Project.Parser.xCode{");
            temp.AppendLine("public class " + EntryPoint + " {");
            temp.AppendLine(GenerateMethod());
            temp.AppendLine("}}");
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
                                      GenerateExecutable = false,
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
                        var message = string.Format("({0}) : {1} @({2},{3})", error.ErrorNumber, error.ErrorText, error.Line,error.Column);
                        Console.WriteLine(message);
                        var ex = new Exception(message);
                        if (Error != null)
                        {
                            Error(ex, SourceCode);
                        }

                        Result += "//" + message + "\r\n";
                    }
                }
                if (Cached.Keys.Contains(Options.InstanceId))
                    Cached[Options.InstanceId] = result;
                else
                    Cached.Add(Options.InstanceId, result);
            }
            return result;
        }


        public static void Release()
        {
            if (Cached != null)
            {
                foreach (var compilerResult in Cached)
                {
                    compilerResult.Value.TempFiles.Delete();
                }
                Cached.Clear();
            }
            var gen = GC.GetGeneration(Cached);
            GC.Collect(gen);
        }
    }
}