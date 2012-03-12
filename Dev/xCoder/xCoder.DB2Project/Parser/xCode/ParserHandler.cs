// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ParserHandler.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;

namespace xCoder.DB2Project.Parser.xCode
{
    public delegate void StatementErrorHandler(Exception ex, string statment);

    public delegate void StatmentOutput(String output);
}