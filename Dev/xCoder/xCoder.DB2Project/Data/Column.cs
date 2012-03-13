// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/Column.cs                                                                   
// *	Created @ 03/09/2012 7:17 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using xCoder.DB2Project.Data.Type;

namespace xCoder.DB2Project.Data
{
    public class Column
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public bool PrimaryKey { get; set; }
        public bool Nullable { get; set; }
        public string DBType { get; set; }
        public object DefaultValue { get; set; }
        public int MaxLength { get; set; }

        public string FieldName { get; set; }

        public string CSharpType
        {
            get { return TypeMap.GetTypeString(DBType, Nullable); }
        }
    }
}