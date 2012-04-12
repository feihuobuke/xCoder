// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/ParserOption.cs                                                                   
// *	Created @ 03/09/2012 6:29 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace SF.xCoder.DB2Project.Parser.xCode
{
    internal class ParserOption : ICloneable
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


        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            return new ParserOption
            {
                Code = Code,
                InstanceId = Guid.NewGuid(),
                Namesapces = Namesapces,
                References = References,
                SourceCode = SourceCode,
                StatementParameters = StatementParameters,
                VariableCollection = VariableCollection,
                VariableParameter = VariableParameter
            };
        }
    }
}