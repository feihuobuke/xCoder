
using System.Collections.Generic;

namespace xCoder.Bean
{
    public class Column
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public bool PrimaryKey { get; set; }
        public bool Nullable { get; set; }
        public string DBType { get; set; }
        public object DefaultValue { get; set; }
        public int MaxLength { get; set; }
        public List<ForeignKey> ForeignKeys { get; set; }
    }
}