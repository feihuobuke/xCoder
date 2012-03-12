// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/CollectionExt.cs                                                                   
// *	Created @ 03/10/2012 5:34 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Specialized;

namespace xCoder.DB2Project.Extension
{
    internal static class CollectionExt
    {
        public static NameValueCollection Convert(this object source)
        {
            var colletion = new NameValueCollection();
            if (source != null)
            {
                var properties = source.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var value = property.GetValue(source, null);

                    colletion.Add(property.Name, (value ?? string.Empty).ToString());
                }
            }
            return colletion;
        }
    }
}