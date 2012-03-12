// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/BuilderParameters.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.IO;
using xCoder.DB2Project.Data;

namespace xCoder.DB2Project.Builder
{
    public class BuilderParameters
    {
        public string Namespace { get; set; }
        public DirectoryInfo OutputDirectory { get; set; }
        public DataBase DataBase { get; set; }
        public FileInfo Template { get; set; }
    }
}