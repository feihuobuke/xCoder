// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/DBTypeAttribute.cs                                                                   
// *	Created @ 03/09/2012 7:36 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.Data;

namespace xCoder.DB2Project.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class DBTypeAttribute : Attribute
    {
        public string DBType { get; set; }
        public DBTypeAttribute(string dbType)
        {
            DBType = dbType;
        }
    }
}