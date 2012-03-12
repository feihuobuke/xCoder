// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/AbsReader.cs                                                                   
// *	Created @ 03/09/2012 8:34 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.Data;

namespace xCoder.DB2Project.Data.Reader
{
    internal abstract class AbsReader : IReader
    {
        protected AbsReader(DBConnection connection)
        {
            Connection = connection.ToDbConnection();
            DBConn = connection;
        }

        protected DBConnection DBConn { get; set; }
        protected IDbCommand Command { get; set; }

        #region IReader Members

        public IDbConnection Connection { get; private set; }

        public virtual DataBase Read()
        {
            return new DataBase {Connection = DBConn, Tables = GetTables()};
        }

        #endregion

        protected abstract List<Table> GetTables();
    }
}