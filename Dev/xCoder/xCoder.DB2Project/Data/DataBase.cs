// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/DataBase.cs                                                                   
// *	Created @ 03/09/2012 7:17 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Generic;

namespace xCoder.DB2Project.Data
{
    public class DataBase
    {
        public DBConnection Connection { get; set; }
        public List<Table> Tables { get; set; }
    }
}