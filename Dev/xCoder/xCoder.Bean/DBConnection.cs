using System.Data;

namespace xCoder.Bean
{
    public class DBConnection
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public bool WindowsAuthorization { get; set; }
        public DataBaseType DBType { get; set; }
    }
}