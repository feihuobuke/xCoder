// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/HBMBuilder.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Xml;
using xCoder.DB2Project.Comm;
using xCoder.DB2Project.Data;
using xCoder.DB2Project.Extension;

namespace xCoder.DB2Project.Builder
{
    public class HBMBuilder : AbsBuilder
    {
        public HBMBuilder(BuilderOption parameters)
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
                var classNode = xml.CreateElement("class");
                var nameAttribute = xml.CreateAttribute("name");
                nameAttribute.Value = AssemblyTypeFormat(table);
                classNode.Attributes.Append(nameAttribute);
                var tableAttribute = xml.CreateAttribute("table");
                tableAttribute.Value = string.Format(string.IsNullOrEmpty(table.Owner) ? "{1}" : "{0}.{1}", table.Owner, table.Name);
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
                    propertyNameAttribute.Value = col.FieldName ?? col.Name;
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
                var parents = table.Parents.Where(t => !string.IsNullOrEmpty(t.ColumnRelated) && !string.IsNullOrEmpty(t.TableRelated)).ToList();
                foreach (var parent in parents)
                {
                    var schema = new XmlNodeSchema
                    {
                        Name = "many-to-one",
                        Properties = new NameValueCollection { { "name", parent.ClassName ?? parent.TableRelated }, { "column", parent.Column }, { "cascade", "none" }, { "unique", "true" }, { "class", AssemblyTypeFormat(parent) }, }
                    };
                    classNode.Write(schema);
                }

                var childs = table.Childs.Where(t => !string.IsNullOrEmpty(t.ColumnRelated) && !string.IsNullOrEmpty(t.TableRelated)).ToList();
                foreach (var child in childs)
                {

                    var schema = new XmlNodeSchema
                                     {
                                         Name = "list",
                                         Properties = new NameValueCollection
                                         {
                                             {"name",child.TableRelated+"List"},
                                             {"table",child.TableRelated},
                                             {"cascade","all-delete-orphan"},
                                             {"lazy","true"},
                                         }
                                     };
                    schema.ChildNodes = new List<XmlNodeSchema>
                                          {
                                              new XmlNodeSchema
                                              {
                                                  Name = "key",
                                                  Properties = new NameValueCollection{{"column",child.ColumnRelated}}
                                              },
                                              new XmlNodeSchema
                                              {
                                                  Name = "one-to-many",
                                                  Properties = new NameValueCollection{{"class", AssemblyTypeFormat(child)}}
                                              }

                                          };
                    classNode.Write(schema);
                }

                root.AppendChild(classNode);
                var fileName = string.Format("{0}{1}.hbm.xml", Directory.FullName, table.Name);
                xml.Save(fileName);
                tmp.Add(fileName);
            }
            return tmp.ToArray();
        }

        protected string AssemblyTypeFormat(Table table)
        {
            var assembly = Parameters.Assembly ?? Parameters.Namespace;
            assembly = string.IsNullOrEmpty(assembly) ? string.Empty : (", " + assembly);
            var @namespace = string.IsNullOrEmpty(Parameters.Namespace) ? string.Empty : (Parameters.Namespace + ".");
            var name = string.IsNullOrEmpty(table.ClassName) ? table.Name : table.ClassName;
            return string.Format("{0}{1}{2}", @namespace, name, assembly);
        }

        protected string AssemblyTypeFormat(TableRelation table)
        {
            var assembly = Parameters.Assembly ?? Parameters.Namespace;
            assembly = string.IsNullOrEmpty(assembly) ? string.Empty : (", " + assembly);
            var @namespace = string.IsNullOrEmpty(Parameters.Namespace) ? string.Empty : (Parameters.Namespace + ".");
            var name = string.IsNullOrEmpty(table.ClassName) ? table.TableRelated : table.ClassName;
            return string.Format("{0}{1}{2}", @namespace, name, assembly);
        }
    }
}