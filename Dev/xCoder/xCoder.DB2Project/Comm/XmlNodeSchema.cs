// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/XmlSchema.cs                                                                   
// *	Created @ 03/13/2012 3:45 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.Collections.Specialized;

namespace xCoder.DB2Project.Comm
{
    public class XmlNodeSchema
    {
        public string Name { get; set; }
        public NameValueCollection Properties { get; set; }
        public List<XmlNodeSchema> ChildNodes { get; set; }
    }
}