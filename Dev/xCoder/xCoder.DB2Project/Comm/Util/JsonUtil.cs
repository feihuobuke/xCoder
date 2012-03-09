// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/JsonUtil.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System.Web.Script.Serialization;

namespace xCoder.DB2Project.Comm.Util
{
    public static class JsonUtil
    {
        public static string Convert(object source)
        {
            var jsonor = new JavaScriptSerializer();
            return jsonor.Serialize(source);
        }
    }
}