using System.Text;

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal static class ParseResultUtil
    {
        public static string ToString(this StringBuilder content, ScopeIndex index)
        {
            return content.ToString(index.StartIndex, index.Length);
        }

        public static string ScopeBody(this StringBuilder content, ScopeIndex index)
        {
            return content.ToString(index.StartIndex + index.Tag.BeginTag.Length,
                                    index.Length - index.Tag.BeginTag.Length - index.Tag.CloseTag.Length);
        }
    }
}