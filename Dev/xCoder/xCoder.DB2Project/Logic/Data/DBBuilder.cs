using System;
using xCoder.Bean;
using xCoder.Logic.Data.MsSql;

namespace xCoder.Logic
{
    public class DBBuilder
    {
        public DBConnection Connection { get; private set; }

        public DBBuilder(DBConnection connection)
        {
            Connection = connection;
        }

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