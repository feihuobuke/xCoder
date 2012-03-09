// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/DBExt.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.Data;
using System.Data.SqlClient;
using xCoder.DB2Project.Data;
using xCoder.DB2Project.Data.Type;

namespace xCoder.DB2Project
{
    public static class DBExt
    {
        public static IDbConnection ToDbConnection(this DBConnection connection)
        {
            IDbConnection tmp = null;
            switch (connection.DBType)
            {
                case DataBaseType.NONE:
                    break;
                case DataBaseType.MSSQL:
                    tmp = connection.WindowsAuthorization
                              ? new SqlConnection(string.Format(
                                  "Server={0};initial catalog={1};Trusted_Connection=SSPI", connection.Server,
                                  connection.Name))
                              : new SqlConnection(string.Format(
                                  "Server={0};initial catalog={1}; user={2};password={3};", connection.Server,
                                  connection.Name, connection.Account, connection.Password));
                    break;
                case DataBaseType.MYSQL:
                    break;
                case DataBaseType.ORICAL:
                    break;
                case DataBaseType.SQLCE:
                    break;
                case DataBaseType.ACCESS:
                    break;
                case DataBaseType.SQLITE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return tmp;
        }
    }
}