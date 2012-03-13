// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/AbsBuilder.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.IO;
using xCoder.DB2Project.Data;

namespace xCoder.DB2Project.Builder
{
    public abstract class AbsBuilder
    {
        protected AbsBuilder(BuilderOption parameters)
        {
            DataBase = parameters.DataBase;
            Directory = parameters.OutputDirectory;
            Parameters = parameters;
            if (Directory != null && !Directory.Exists)
            {
                Directory.Create();
            }
        }

        public DataBase DataBase { get; protected set; }
        public DirectoryInfo Directory { get; protected set; }
        public BuilderOption Parameters { get; set; }

        /// <summary>
        ///   Build To File
        /// </summary>
        /// <returns> File Path </returns>
        public abstract string[] Build();
    }
}