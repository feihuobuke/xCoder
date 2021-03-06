﻿// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/BuilderParameters.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.IO;
using SF.xCoder.DB2Project.Data;
using SF.xCoder.DB2Project.Data.Type;

namespace SF.xCoder.DB2Project.Builder
{
    public class BuilderOption
    {
        public string Namespace { get; set; }
        public string Assembly { get; set; }
        public DirectoryInfo OutputDirectory { get; set; }
        public DataBase DataBase { get; set; }
        public FileInfo Template { get; set; }
        public string FileNameFormat { get; set; }
        public FileDepend FileDependency { get; set; }
        public bool Override { get; set; }
    }
}