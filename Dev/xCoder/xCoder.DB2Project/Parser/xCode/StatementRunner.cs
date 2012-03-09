using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;

namespace xCoder.Parser.xCode
{
    internal class StatementRunner
    {
        public const string EntryPoint = "CodeMethods";
        protected const string Invoker = "CodeInvoker";

        public StatementRunner(XCoderOptions options, string sourceCode)
        {
            Options = options;
            SourceCode = sourceCode;
        }

        public XCoderOptions Options { get; protected set; }
        public string Result { get; protected set; }
        public int ReturnCode { get; protected set; }
        public bool Successed { get; protected set; }

        public string SourceCode { get; protected set; }

        public event StatementErrorHandler Error;
        public event StatmentOutput Output;

        public void Execute(params object[] objects)
        {
            CompilerResults result = Compile();
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
                    var message = string.Format("({0}) : {1} @({2},{3})", error.ErrorNumber, error.ErrorText, error.Line, error.Column);
                    Console.WriteLine(message);
                    var ex = new Exception(message); if (Error != null)
                    {
                        Error(ex, SourceCode);
                    }
                }
            }
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
            string tmp = "public string " + Invoker + " (DataBase DataBase,Table Table){ string Output=string.Empty;";
            tmp += SourceCode;
            tmp += "return Output;}";
            return tmp;
        }

        protected string GenerateType()
        {
            string tmp = Options.Namesapces.Cast<string>().Aggregate("using System;",
                                                                     (current, namesapce) =>
                                                                     current + string.Format("using {0};", namesapce));
            tmp += "public class " + EntryPoint + " {";
            tmp += GenerateMethod();
            tmp += "}";
            return tmp;
        }

        protected CompilerResults Compile()
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
            return CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(options, source);
        }
    }
}