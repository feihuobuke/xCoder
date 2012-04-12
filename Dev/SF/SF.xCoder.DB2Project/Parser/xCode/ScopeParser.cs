// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ScopeParser.cs                                                                   
// *	Created @ 03/22/2012 7:09 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal class ScopeParser : AbsParser
    {
        /// <summary>
        ///   Global Statement
        /// </summary>
        /// <param name="options"> </param>
        public ScopeParser(ParserOption options)
            : base(options, new ScopeTag {BeginTag = "<#@", CloseTag = "@#>"})
        {
        }

        /// <summary>
        ///   Customize Statement
        /// </summary>
        /// <param name="options"> </param>
        /// <param name="tag"> </param>
        public ScopeParser(ParserOption options, ScopeTag tag)
            : base(options, tag)
        {
        }
    }
}