// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/IReader.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Data;
using xCoder.DB2Project.Data;

namespace xCoder.DB2Project
{
    public interface IReader
    {
        IDbConnection Connection { get; }
        DataBase Read();
    }
}