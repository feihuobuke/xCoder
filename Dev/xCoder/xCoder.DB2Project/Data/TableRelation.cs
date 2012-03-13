// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/TableRelation.cs                                                                   
// *	Created @ 03/13/2012 1:55 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************
namespace xCoder.DB2Project.Data
{
    public class TableRelation
    {
        public string Column { get; set; }
        public string TableRelated { get; set; }
        public string ColumnRelated { get; set; }
        public string ClassName { get; set; }
    }
}