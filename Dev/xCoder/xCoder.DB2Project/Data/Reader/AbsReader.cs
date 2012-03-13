// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/AbsReader.cs                                                                   
// *	Created @ 03/09/2012 8:34 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using xCoder.DB2Project.Comm.Util;

namespace xCoder.DB2Project.Data.Reader
{
    internal abstract class AbsReader : IReader
    {
        protected AbsReader(DBConnection connection)
        {
            Connection = connection.ToDbConnection();
            DBConn = connection;
        }

        protected DBConnection DBConn { get; set; }
        protected IDbCommand Command { get; set; }

        #region IReader Members

        public IDbConnection Connection { get; private set; }

        public virtual DataBase Read()
        {
            var tables = GetTables();
            foreach (var table in tables)
            {
                var tblName = table.Name;
                table.ClassName = StringUtil.ApplyCSharpNaming(tblName);
                foreach (var column in table.Columns)
                {
                    column.FieldName = StringUtil.ApplyCSharpNaming(column.Name);
                }
                var childTables = tables.Where(t => t.Parents.Count > 0 &&
                    t.Parents.Any(x => x.TableRelated.Equals(tblName, StringComparison.OrdinalIgnoreCase))).ToList();
                foreach (var childTable in childTables)
                {
                    foreach (var child in childTable.Parents)
                    {
                        table.Childs.Add(new TableRelation
                                              {
                                                  Column = child.ColumnRelated,
                                                  ColumnRelated = child.Column,
                                                  TableRelated = childTable.Name,
                                                  ClassName = StringUtil.ApplyCSharpNaming(childTable.Name)
                                              });
                    }
                }
            }

            return new DataBase { Connection = DBConn, Tables = tables };
        }

        #endregion

        protected abstract List<Table> GetTables();
    }
}