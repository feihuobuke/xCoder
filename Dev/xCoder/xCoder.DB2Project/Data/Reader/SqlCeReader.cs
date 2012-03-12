// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/SqlCeReader.cs                                                                   
// *	Created @ 03/12/2012 10:32 AM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;

namespace xCoder.DB2Project.Data.Reader
{
    internal class SqlCeReader : AbsReader
    {
        public SqlCeReader(DBConnection connection)
            : base(connection)
        {
            Command = new SqlCeCommand(GetCmd(), (SqlCeConnection)Connection);
        }

        protected override List<Table> GetTables()
        {
            var tmp = new List<Table>();
            using (Command)
            {
                Command.CommandText = GetCmd();
                try
                {
                    Command.Connection.Open();
                    using (var reader = Command.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                var tableName = reader["TABLE_NAME"] as string;
                                if (string.IsNullOrEmpty(tableName))
                                {
                                    continue;
                                }
                                var table =
                                    tmp.FirstOrDefault(
                                        t => t.Name.Equals(tableName, StringComparison.CurrentCultureIgnoreCase)) ??
                                    new Table { Name = tableName, Columns = new List<Column>() };
                                if (table.Columns == null)
                                {
                                    table.Columns = new List<Column>();
                                }
                                var col = new Column();
                                col.Name = reader["COLUMN_NAME"] as string;
                                col.DBType = reader["DATA_TYPE"] as string;
                                col.DefaultValue = reader["COLUMN_NAME"] as string;
                                var index = (reader["ORDINAL_POSITION"] as string) ?? "0";
                                col.Index = int.Parse(index);
                                var length = (reader["MAXIMUM_LENGTH"] as string) ?? "-1";
                                col.MaxLength = int.Parse(length);
                                col.Nullable = ((reader["IS_NULLABLE"] as string) ?? string.Empty).Equals("YES",StringComparison.OrdinalIgnoreCase);
                                //col.Owner = row["TABLE_SCHEMA"] as string;
                                col.PrimaryKey = ((reader["PRIMARY_KEY"] as string) ?? string.Empty).Equals("YES",StringComparison.OrdinalIgnoreCase);
                                var foreignTable = reader["FOREIGN_TALBE"] as string;
                                var foreignColumn = reader["FOREIGN_COLUMN"] as string;
                                col.ForeignKeys = new List<ForeignKey>();
                                if (!string.IsNullOrEmpty(foreignColumn) && !string.IsNullOrEmpty(foreignTable))
                                {
                                    col.ForeignKeys = new List<ForeignKey>
                                                      {
                                                          new ForeignKey
                                                              {
                                                                  ForeignColumn = foreignColumn,
                                                                  ForeignTable = foreignTable
                                                              }
                                                      };
                                }
                                table.Columns.Add(col);
                                if (!tmp.Any(t => t.Name.Equals(tableName, StringComparison.CurrentCultureIgnoreCase)))
                                {
                                    tmp.Add(table);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            throw;
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
                finally
                {
                    if (Command.Connection.State != ConnectionState.Closed)
                        Command.Connection.Close();
                }
            }

            return tmp;
        }

        protected string GetCmd()
        {
            var tmp = string.Empty;
            using (
                var cmdStream =
                    GetType().Assembly.GetManifestResourceStream("xCoder.DB2Project.Resource.SQLCE.Columns.sql"))
                if (cmdStream != null)
                    using (var reader = new StreamReader(cmdStream))
                    {
                        tmp = reader.ReadToEnd();
                        reader.Close();
                        cmdStream.Close();
                    }
            return tmp;
        }
    }
}