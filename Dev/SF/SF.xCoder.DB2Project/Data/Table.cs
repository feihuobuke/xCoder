// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/Table.cs                                                                   
// *	Created @ 03/09/2012 7:17 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using SF.xCoder.DB2Project.Data.Type;

namespace SF.xCoder.DB2Project.Data
{
    public class Table
    {
        public Table()
        {
            Parents = new List<TableRelation>();
            Childs = new List<TableRelation>();
            Columns = new List<Column>();
        }

        public string Owner { get; set; }
        public string Name { get; set; }
        public TableType Type { get; set; }
        public List<Column> Columns { get; set; }
        public List<TableRelation> Parents { get; set; }
        public List<TableRelation> Childs { get; set; }

        public string ClassName { get; set; }
    }
}