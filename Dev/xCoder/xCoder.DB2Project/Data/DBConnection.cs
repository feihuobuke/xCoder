// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/DBConnection.cs                                                                   
// *	Created @ 03/09/2012 7:17 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using xCoder.DB2Project.Data.Type;

namespace xCoder.DB2Project.Data
{
    public class DBConnection
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public bool WindowsAuthorization { get; set; }
        public DataBaseType DBType { get; set; }
    }
}