// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/SolutionBuilder.cs                                                                   
// *	Created @ 03/10/2012 6:44 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

namespace xCoder.DB2Project.Builder
{
    public class SolutionBuilder : FileBuilder
    {
        public SolutionBuilder(BuilderParameters parameters) : base(parameters, ".sln")
        {
        }
    }
}