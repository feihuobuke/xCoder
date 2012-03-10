// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/XCoderOptions.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace xCoder.DB2Project.Parser.xCode
{
    public class ParserOption
    {
        public ParserOption()
        {
            Namesapces = new StringCollection();
            References = new StringCollection();
            Code = null;
            VariableCollection = new NameValueCollection();
            InstanceId = Guid.NewGuid();
        }
        public Guid InstanceId { get; private set; }
        public StringCollection Namesapces { get; set; }
        public StringCollection References { get; set; }
        public FileInfo SourceCode { get; set; }
        internal StringBuilder Code { get; set; }

        public object VariableParameter { get; set; }
        public object[] StatementParameters { get; set; }
        public NameValueCollection VariableCollection { get; set; }
    }
}