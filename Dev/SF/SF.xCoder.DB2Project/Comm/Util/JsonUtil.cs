// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/JsonUtil.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************


namespace SF.xCoder.DB2Project.Comm.Util
{
    public static class JsonUtil
    {
        public static string Convert(object source)
        {
            //var jsonor = new JavaScriptSerializer();
            //return jsonor.Serialize(source);
            return source as string;
        }
    }
}