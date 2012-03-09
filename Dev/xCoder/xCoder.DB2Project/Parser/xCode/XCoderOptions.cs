using System.Collections.Specialized;
using System.IO;

namespace xCoder.Parser.xCode
{
    public class XCoderOptions
    {
        public XCoderOptions()
        {
            Namesapces = new StringCollection();
            References = new StringCollection();
            Code = null;
        }
        public StringCollection Namesapces { get; set; }
        public StringCollection References { get; set; }
        public FileInfo SourceCode { get; set; }
        internal TextReader Code { get; set; }

        public object VariableParameter { get; set; }
        public object[] StatementParameters { get; set; }
    }
}