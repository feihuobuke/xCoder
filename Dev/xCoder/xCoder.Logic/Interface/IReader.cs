using System.Data;
using xCoder.Bean;

namespace xCoder.Logic
{
    public interface IReader
    {
        IDbConnection Connection { get; }
        DataBase Read();
    }
}