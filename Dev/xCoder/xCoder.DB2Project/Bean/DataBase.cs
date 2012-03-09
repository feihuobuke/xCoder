using System.Collections.Generic;

namespace xCoder.Bean
{
    public class DataBase
    {
        public DBConnection Connection { get; set; }
        public List<Table> Tables { get; set; }
    }
}