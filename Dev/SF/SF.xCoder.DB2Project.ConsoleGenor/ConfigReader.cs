using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace SF.xCoder.DB2Project.ConsoleGenor
{
    public class ConfigReader
    {
        public FileInfo ConfigFile { get; protected set; }

        public ConfigReader(FileInfo configFile)
        {
            ConfigFile = configFile;
        }

        void Load()
        {
            var xml = XDocument.Parse(File.ReadAllText(ConfigFile.FullName));
            var conn = xml.Nodes().InDocumentOrder().Ancestors("DBConnection");

        }
    }
}