// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ClassBuilder.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

namespace xCoder.DB2Project.Builder
{
    public class ClassBuilder : FileBuilder
    {
        public ClassBuilder(BuilderParameters parameters)
            : base(parameters, ".cs")
        {
        }
    }
}