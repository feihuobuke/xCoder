using System;
using System.Text;

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal class ParserEventArgs : EventArgs
    {
        public StringBuilder Result { get; internal set; }
        public StringBuilder Replacement { get; set; }
        public bool Replace { get; set; }
        public StringBuilder Body { get; internal set; }

        public ScopeIndex Index { get; internal set; }
        public ScopeTag Tag { get; internal set; }

        public ParserOption Option { get; internal set; }
    }
}