using System.Web.Script.Serialization;

namespace xCoder
{
    public static class JsonUtil
    {
        public static string Convert(object source)
        {
            var jsonor = new JavaScriptSerializer();
            return jsonor.Serialize(source);
        }
    }
}