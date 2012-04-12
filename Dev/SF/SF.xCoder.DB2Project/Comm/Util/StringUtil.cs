// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/StringUtil.cs                                                                   
// *	Created @ 03/13/2012 4:47 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Linq;

namespace SF.xCoder.DB2Project.Comm.Util
{
    public class StringUtil
    {
        public static string ApplyCSharpNaming(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            const string invalidChar = "~!@#$%^&*()+<>?:\"|{}[];'\\,./";
            var names = name.Split(invalidChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var nums = "0123456789".ToArray();
            for (int i = 0; i < names.Length; i++)
            {
                var tmp = names[i];
                if (i == 0 && tmp.Length > 0)
                {
                    while (nums.Any(t => t == tmp[0]))
                    {
                        tmp = tmp.Remove(0, 1);
                    }
                }
                if (tmp.Length <= 2)
                {
                    tmp = tmp.ToUpper();
                }
                else
                {
                    var cTemp = tmp.ToArray();
                    for (int j = 0; j < cTemp.Length; j++)
                    {
                        if (char.IsUpper(cTemp[j]) && j == 0) break;
                        if (char.IsUpper(cTemp[j]) || char.IsNumber(cTemp[j])) continue;
                        cTemp[j] = char.ToUpper(cTemp[j]);
                        break;
                    }

                    tmp = new string(cTemp);
                }

                names[i] = tmp;
            }
            var cleanName = names.Aggregate(string.Empty, (current, s) => current + s);
            if (string.IsNullOrEmpty(cleanName))
            {
                cleanName = "INVALID_CLASS_NAME";
            }
            return cleanName;
        }
    }
}