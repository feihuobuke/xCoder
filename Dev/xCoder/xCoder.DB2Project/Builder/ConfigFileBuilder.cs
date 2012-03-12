// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ConfigFileBuilder.cs                                                                   
// *	Created @ 03/10/2012 6:45 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

namespace xCoder.DB2Project.Builder
{
    public class ConfigFileBuilder : AbsBuilder
    {
        public ConfigFileBuilder(BuilderParameters parameters) : base(parameters)
        {
        }

        public override string[] Build()
        {
            //todo: build .config file for projects
            return new string[0];
        }
    }
}