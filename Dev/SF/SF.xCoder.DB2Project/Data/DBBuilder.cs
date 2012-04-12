// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/DBBuilder.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using SF.xCoder.DB2Project.Data.Reader;
using SF.xCoder.DB2Project.Data.Type;

namespace SF.xCoder.DB2Project.Data
{
    public class DBBuilder
    {
        public DBBuilder(DBConnection connection)
        {
            Connection = connection;
        }

        public DBConnection Connection { get; private set; }

        public DataBase Build()
        {
            var tmp = new DataBase();
            switch (Connection.DBType)
            {
                case DataBaseType.NONE:
                    break;
                case DataBaseType.MSSQL:
                    tmp = new MsSqlReader(Connection).Read();
                    break;
                case DataBaseType.MYSQL:
                    break;
                case DataBaseType.ORICAL:
                    break;
                case DataBaseType.SQLCE:
                    tmp = new SqlCeReader(Connection).Read();
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