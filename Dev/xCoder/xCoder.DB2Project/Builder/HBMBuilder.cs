// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/NHibernateBuilder.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follow BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace xCoder.DB2Project.Builder
{
    public class HBMBuilder : AbsBuilder
    {
        public HBMBuilder(BuilderParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns> Built Files </returns>
        public override string[] Build()
        {
            return HBMFileGenerate();
        }
        protected string[] HBMFileGenerate()
        {
            var tmp = new List<string>();
            foreach (var table in DataBase.Tables)
            {
                var xml = new XmlDocument();
                var version = xml.CreateXmlDeclaration("1.0", "utf-8", "");
                xml.AppendChild(version);
                var root = xml.CreateElement("hibernate-mapping", "urn:nhibernate-mapping-2.2");
                xml.AppendChild(root);
                var classNode = xml.CreateElement("class", null);
                var nameAttribute = xml.CreateAttribute("name");
                nameAttribute.Value = string.Format("{0}.{1},{0}", Parameters.Namespace, table.Name);
                classNode.Attributes.Append(nameAttribute);
                var tableAttribute = xml.CreateAttribute("table");
                tableAttribute.Value = string.Format("{0}.{1}", table.Owner, table.Name);
                classNode.Attributes.Append(tableAttribute);
                foreach (var col in table.Columns.OrderBy(t => t.Index).ToList())
                {
                    XmlElement property;
                    if (col.PrimaryKey)
                    {
                        property = xml.CreateElement("id");
                        var genor = xml.CreateElement("generator");
                        var genorClassAttribute = xml.CreateAttribute("class");
                        genorClassAttribute.Value = col.DBType.Equals("int") ? "identity" : "guid";
                        genor.Attributes.Append(genorClassAttribute);
                        property.AppendChild(genor);
                    }
                    else
                    {
                        property = xml.CreateElement("property");
                    }
                    var propertyNameAttribute = xml.CreateAttribute("name");
                    propertyNameAttribute.Value = col.Name;
                    property.Attributes.Append(propertyNameAttribute);


                    var propertyColumnAttribute = xml.CreateAttribute("column");
                    propertyColumnAttribute.Value = col.Name;
                    property.Attributes.Append(propertyColumnAttribute);

                    if (col.Nullable)
                    {
                        var propertyNotNullAttribute = xml.CreateAttribute("not-null");
                        propertyNotNullAttribute.Value =
                            (!col.Nullable).ToString(CultureInfo.InvariantCulture).ToLower();
                        property.Attributes.Append(propertyNotNullAttribute);
                    }
                    classNode.AppendChild(property);
                }
                var manyToOnes = table.Columns.Where(t => t.ForeignKeys.Count > 0).ToList();
                foreach (var manyToOne in manyToOnes)
                {
                    foreach (var foreignKey in manyToOne.ForeignKeys)
                    {
                        var manyToOneNode = xml.CreateElement("many-to-one");
                        var manyToOneNameAttribute = xml.CreateAttribute("name");
                        manyToOneNameAttribute.Value = foreignKey.ForeignTable;
                        manyToOneNode.Attributes.Append(manyToOneNameAttribute);

                        var manyToOneColumnAttribute = xml.CreateAttribute("column");
                        manyToOneColumnAttribute.Value = manyToOne.Name;
                        manyToOneNode.Attributes.Append(manyToOneColumnAttribute);

                        var manyToOneCascadeAttribute = xml.CreateAttribute("cascade");
                        manyToOneCascadeAttribute.Value = "none";
                        manyToOneNode.Attributes.Append(manyToOneCascadeAttribute);

                        var manyToOneUniqueAttribute = xml.CreateAttribute("unique");
                        manyToOneUniqueAttribute.Value = "true";
                        manyToOneNode.Attributes.Append(manyToOneUniqueAttribute);

                        var manyToOneClassAttribute = xml.CreateAttribute("class");
                        manyToOneClassAttribute.Value = string.Format("{0}.{1},{0}", Parameters.Namespace,
                                                                      foreignKey.ForeignTable);
                        manyToOneNode.Attributes.Append(manyToOneClassAttribute);
                        classNode.AppendChild(manyToOneNode);
                    }
                }

                root.AppendChild(classNode);
                var fileName = string.Format("{0}{1}.hbm.xml", Directory.FullName, table.Name);
                xml.Save(fileName);
                tmp.Add(fileName);
            }
            return tmp.ToArray();
        }
    }
}