// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ProjectBuilder.cs                                                                   
// *	Created @ 03/10/2012 6:43 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

namespace SF.xCoder.DB2Project.Builder
{
    public class ProjectBuilder : FileBuilder
    {
        public ProjectBuilder(BuilderOption parameters)
            : base(parameters, ".csproj")
        {
        }
    }

    public enum ProjectType
    {
        Libarary,
        WebApplication
    }
}