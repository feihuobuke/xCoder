// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/MsSqlReader.cs                                                                   
// *	Created @ 03/09/2012 7:16 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using xCoder.DB2Project.Data.Type;

namespace xCoder.DB2Project.Data.Reader
{
    internal class MsSqlReader : AbsReader
    {
        public MsSqlReader(DBConnection connection)
            : base(connection)
        {
            Command = new SqlCommand {Connection = (SqlConnection) Connection};
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
                        var data = new DataSet();
                        data.Load(reader, LoadOption.OverwriteChanges, "");
                        if (data.Tables.Count > 0)
                        {
                            var tables =
                                data.Tables[0].Rows.OfType<DataRow>().GroupBy(t => t["TABLE_NAME"]).ToDictionary(
                                    t => t.Key as string, t => t.ToList());
                            foreach (var table in tables)
                            {
                                try
                                {
                                    var tbl = new Table();
                                    tbl.Name = table.Key; // row["TABLE_NAME"] as string;
                                    tbl.Columns = new List<Column>();
                                    foreach (var row in table.Value)
                                    {
                                        tbl.Owner = row["TABLE_SCHEMA"] as string;
                                        var type = row["TABLE_TYPE"] as string;
                                        tbl.Type = type != null &&
                                                   type.Equals("VIEW", StringComparison.CurrentCultureIgnoreCase)
                                                       ? TableType.View
                                                       : TableType.Default;
                                        var col = new Column();
                                        col.Name = row["COLUMN_NAME"] as string;
                                        col.DBType = row["DATA_TYPE"] as string;
                                        col.DefaultValue = row["COLUMN_NAME"] as string;
                                        var index = (row["ORDINAL_POSITION"] as string) ?? "0";
                                        col.Index = int.Parse(index);
                                        var length = (row["MAXIMUM_LENGTH"] as string) ?? "-1";
                                        col.MaxLength = int.Parse(length);
                                        col.Nullable = ((row["IS_NULLABLE"] as string) ?? string.Empty).Equals("YES",
                                                                                                               StringComparison
                                                                                                                   .
                                                                                                                   OrdinalIgnoreCase);
                                        col.Owner = row["TABLE_SCHEMA"] as string;
                                        col.PrimaryKey = ((row["PRIMARY_KEY"] as string) ?? string.Empty).Equals("YES",
                                                                                                                 StringComparison
                                                                                                                     .
                                                                                                                     OrdinalIgnoreCase);
                                        var foreignTable = row["FOREIGN_TALBE"] as string;
                                        var foreignColumn = row["FOREIGN_COLUMN"] as string;
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
                                        tbl.Columns.Add(col);
                                    }
                                    tmp.Add(tbl);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                        }
                        data.Dispose();
                        reader.Close();
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
                    GetType().Assembly.GetManifestResourceStream("xCoder.DB2Project.Resource.MSSQL.Columns.sql"))
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