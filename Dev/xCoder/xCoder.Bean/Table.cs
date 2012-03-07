using System.Collections.Generic;

namespace xCoder.Bean
{
    public class Table
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public TableType Type { get; set; }
        public List<Column> Columns { get; set; }
    }
}