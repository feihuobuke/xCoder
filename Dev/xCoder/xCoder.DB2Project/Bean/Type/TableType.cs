using System.ComponentModel;

namespace xCoder.Bean
{
    public enum TableType
    {
        [Description("Table")]
        Default,
        [Description("View")]
        View,
    }
}