// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/Table.cs                                                                   
// *	Created @ 03/09/2012 7:17 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using xCoder.DB2Project.Data.Type;

namespace xCoder.DB2Project.Data
{
    public class Table
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public TableType Type { get; set; }
        public List<Column> Columns { get; set; }
    }
}