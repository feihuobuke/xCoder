// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/TableType.cs                                                                   
// *	Created @ 03/09/2012 7:17 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.ComponentModel;

namespace SF.xCoder.DB2Project.Data.Type
{
    public enum TableType
    {
        [Description("Table")] Default,
        [Description("View")] View,
    }
}