namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal class ScopeIndex
    {
        public ScopeIndex(ScopeTag tag)
        {
            Tag = tag;
        }

        public ScopeTag Tag { get; protected set; }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public int Length
        {
            get { return EndIndex - StartIndex; }
        }

        public bool Contains(ScopeIndex index)
        {
            return StartIndex < index.StartIndex && EndIndex > index.EndIndex;
        }

        public static bool operator >(ScopeIndex index1, ScopeIndex index2)
        {
            return index1.Contains(index2);
        }

        public static bool operator <(ScopeIndex index1, ScopeIndex index2)
        {
            return index2.Contains(index1);
        }

        public new string ToString()
        {
            return string.Format("Start:{0} - Close: {1}", StartIndex, EndIndex);
        }
    }
}