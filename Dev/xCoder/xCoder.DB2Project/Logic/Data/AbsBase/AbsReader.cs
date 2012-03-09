using System.Collections.Generic;
using System.Data;
using xCoder.Bean;

namespace xCoder.Logic.AbsBase
{
    public abstract class AbsReader : IReader
    {
        public IDbConnection Connection { get; private set; }
        protected DBConnection DBConn { get; set; }
        protected IDbCommand Command { get; set; }

        protected AbsReader(DBConnection connection)
        {
            Connection = connection.ToDbConnection();
            DBConn = connection;
        }

        protected abstract List<Table> GetTables();
        public virtual DataBase Read()
        {
            return new DataBase { Connection = DBConn, Tables = GetTables() };
        }
    }
}