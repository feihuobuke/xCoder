// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/XmlExt.cs                                                                   
// *	Created @ 03/13/2012 3:42 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Xml;
using xCoder.DB2Project.Comm;

namespace xCoder.DB2Project.Extension
{
    public static class XmlExt
    {
        public static XmlDocument Create(this XmlDocument xml)
        {
            xml = xml ?? new XmlDocument();
            var version = xml.CreateXmlDeclaration("1.0", "utf-8", "");
            xml.AppendChild(version);
            return xml;
        }

        public static void Write(this XmlElement applyNode, XmlNodeSchema schema)
        {
            var xml = applyNode.OwnerDocument;
            if (xml == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(schema.Name))
            {
                return;
            }
            var node = xml.CreateElement(schema.Name);
            foreach (string property in schema.Properties)
            {
                var value = schema.Properties[property];
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                var attribute = xml.CreateAttribute(property);
                attribute.Value = value;
                node.Attributes.Append(attribute);
            }
            applyNode.AppendChild(node);
            if (schema.ChildNodes == null) return;
            foreach (var childNode in schema.ChildNodes)
            {
                Write(node, childNode);
            }
        }
    }
}