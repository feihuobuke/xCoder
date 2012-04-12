namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal struct ScopeTag
    {
        public string BeginTag { get; set; }
        public string CloseTag { get; set; }
        public bool BeginDropInNextSpace { get; set; }
    }
}