using System.IO;
using xCoder.Bean;

namespace xCoder.Logic.Builder
{
    public abstract class AbsBuilder
    {
        public DataBase DataBase { get; protected set; }
        public DirectoryInfo Directory { get; protected set; }
        public BuilderParameters Parameters { get; set; }

        protected AbsBuilder(BuilderParameters parameters)
        {
            DataBase = parameters.DataBase;
            Directory = parameters.OutputDirectory;
            Parameters = parameters;
            if (Directory != null && !Directory.Exists)
            {
                Directory.Create();
            }
        }
        /// <summary>
        /// Build To File
        /// </summary>
        /// <returns>File Path</returns>
        public abstract string[] Build();
    }
}