using System.Collections;
using System.Collections.Specialized;

namespace xCoder.Parser.xCode
{
    internal class ValueCollection : NameValueCollection
    {

    }

    internal static class ValueCollectionExt
    {
        public static ValueCollection Convert(this object source)
        {
            var colletion = new ValueCollection();
            if (source != null)
            {
                var properties = source.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var value = property.GetValue(source, null);

                    colletion.Add(property.Name, (value??string.Empty).ToString());
                }
            }
            return colletion;
        }
    }
}