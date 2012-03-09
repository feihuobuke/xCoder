using System.Collections.Specialized;
using System.IO;
using xCoder.Bean;

namespace xCoder.Logic.Builder
{
    public class BuilderParameters
    {
        public string Namespace { get; set; }
        public DirectoryInfo OutputDirectory { get; set; }
        public DataBase DataBase { get; set; }
        public FileInfo Template { get; set; }
    }
}