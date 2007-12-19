#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

using Telesophy.Alexandria.Persistence;

namespace Alexandria.Persistence.SQLite
{
	public class SQLiteEngine : IPersistenceEngine
	{
		#region Constructors
		public SQLiteEngine()
		{
			if (ConfigurationManager.AppSettings[KEY_DB_DIR] != null)
				databaseDirectory = ConfigurationManager.AppSettings[KEY_DB_DIR].ToString();
			else databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + System.IO.Path.DirectorySeparatorChar + "Alexandria";

			if (!Directory.Exists(databaseDirectory))
				Directory.CreateDirectory(databaseDirectory);

			if (ConfigurationManager.AppSettings[KEY_DB_NAME] != null)
				databaseName = ConfigurationManager.AppSettings[KEY_DB_NAME].ToString();
			else databaseName = DEFAULT_DB_NAME;

			if (ConfigurationManager.AppSettings[KEY_DB_UPDATE] != null)
				databaseUpdate = ConfigurationManager.AppSettings[KEY_DB_UPDATE].ToString();
			else databaseUpdate = DEFAULT_DB_UPDATE;

			if (ConfigurationManager.AppSettings[KEY_DB_SYNC] != null)
				databaseSync = ConfigurationManager.AppSettings[KEY_DB_SYNC].ToString();
			else databaseSync = DEFAULT_DB_SYNC;

			databasePath = databaseDirectory + Path.DirectorySeparatorChar + databaseName;
		}
		#endregion

		#region Private Constants
		private const string KEY_DB_DIR = "DatabaseDirectory";
		private const string KEY_DB_NAME = "DatabaseName";
		private const string KEY_DB_UPDATE = "DatabaseUpdate";
		private const string KEY_DB_SYNC = "DatabaseSync";
		private const string CON_STRING_FORMAT = "Data Source={0};New={1};UTF8Encoding=True;Version=3";
		private const string DEFAULT_DB_NAME = "Catalog.db";
		private const string DEFAULT_DB_UPDATE = "Automatic";
		private const string DEFAULT_DB_SYNC = "Manual";
		#endregion

		#region Private Fields
		private string databaseDirectory;
		private string databaseName;
		private string databaseUpdate;
		private string databaseSync;
		private string databasePath;
		#endregion

		#region Private Methods
		private string GetConnectionString()
		{
			bool databaseIsNew = false;
			databaseIsNew = (!File.Exists(databasePath));
			return string.Format(CON_STRING_FORMAT, databasePath, databaseIsNew);
		}

		private SQLiteConnection GetSQLiteConnection()
		{
			return new System.Data.SQLite.SQLiteConnection(GetConnectionString());
		}

		private string GetColumnTypeName(Type type)
		{
			string name = "TEXT";

			if (type == typeof(bool) ||
				type == typeof(sbyte) ||
				type == typeof(byte) ||
				type == typeof(short) ||
				type == typeof(ushort) ||
				type == typeof(int) ||
				type == typeof(uint) ||
				type == typeof(long) ||
				type == typeof(ulong) ||
				type == typeof(DateTime) ||
				type == typeof(TimeSpan))
				name = "INTEGER";

			if (type == typeof(float) ||
				type == typeof(double) ||
				type == typeof(decimal))
				name = "REAL";

			return name;
		}

		private object GetValue(DataRow row, int index)
		{
			if (row != null && index >= 0 && index < row.Table.Columns.Count)
			{
				DataColumn col = row.Table.Columns[index];
				string typeName = GetColumnTypeName(col.DataType);

				switch (typeName)
				{
					case "TEXT":
						if (row[index] != null && row[index] != DBNull.Value)
							return row[index].ToString();
						else return string.Empty;
					case "INTEGER":
						if (col.DataType == typeof(DateTime))
							return ((DateTime)row[index]).ToFileTimeUtc();
						else if (col.DataType == typeof(TimeSpan))
							return ((TimeSpan)row[index]).Ticks;
						else return Convert.ToInt32(row[index]);
					case "REAL":
						return Convert.ToDecimal(row[index]);
					default:
						break;
				}
			}
			return string.Empty;
		}

		private SQLiteCommand GetSelectCommand(DataTable table, Guid id)
		{
			if (table != null && table.PrimaryKey.Length > 0)
			{
				SQLiteCommand command = new SQLiteCommand(GetSQLiteConnection());

				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("SELECT * FROM {0}", table.TableName);
				if (id != default(Guid))
				{
					sql.AppendFormat(" WHERE {1} = @Id", table.PrimaryKey[0].ColumnName);
					command.Parameters.Add(new SQLiteParameter("@Id", id.ToString()));
				}

				command.CommandText = sql.ToString();

				return command;
			}
			return null;
		}

		private SQLiteCommand GetSelectCommand(DataTable table, string filter)
		{
			if (table != null)
			{
				SQLiteCommand command = new SQLiteCommand(GetSQLiteConnection());

				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("SELECT * FROM {0}", table.TableName);
				if (!string.IsNullOrEmpty(filter))
				{
					sql.AppendFormat(" WHERE {0}", filter);
					//command.Parameters.Add(new SQLiteParameter("@Id", id.ToString()));
				}

				command.CommandText = sql.ToString();

				return command;
			}
			return null;
		}

		private DateTime GetDateTime(object data)
		{
			try
			{
				return DateTime.FromFileTimeUtc(Convert.ToInt64(data));
			}
			catch
			{
				return default(DateTime);
			}
		}

		private TimeSpan GetTimeSpan(object data)
		{
			try
			{
				return TimeSpan.FromTicks(Convert.ToInt64(data));
			}
			catch
			{
				return default(TimeSpan);
			}
		}

		private Uri GetUri(object data)
		{
			try
			{
				return new Uri(data.ToString());
			}
			catch
			{
				return null;
			}
		}

		private Guid GetGuid(object data)
		{
			try
			{
				return new Guid(data.ToString());
			}
			catch
			{
				return default(Guid);
			}
		}

		private void FillTable(DataTable dataTable, Guid id, string filter)
		{
			if (dataTable != null)
			{
				dataTable.Rows.Clear();
				SQLiteCommand cmd = null;

				if (!string.IsNullOrEmpty(filter))
					cmd = GetSelectCommand(dataTable, filter);
				else cmd = GetSelectCommand(dataTable, id);

				if (cmd != null)
				{
					using (cmd.Connection)
					{
						cmd.Connection.Open();

						using (SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
						{
							if (reader != null && reader.HasRows)
							{
								while (reader.Read())
								{
									DataRow row = dataTable.NewRow();

									for (int i = 0; i < dataTable.Columns.Count; i++)
									{
										object data = reader[i];
										if (dataTable.Columns[i].DataType == typeof(DateTime))
											data = GetDateTime(data);
										else if (dataTable.Columns[i].DataType == typeof(TimeSpan))
											data = GetTimeSpan(data);
										else if (dataTable.Columns[i].DataType == typeof(Uri))
											data = GetUri(data);
										else if (dataTable.Columns[i].DataType == typeof(Guid))
											data = GetGuid(data);

										row[i] = data;
									}

									dataTable.Rows.Add(row);
								}
							}
						}
					}
				}
			}
		}
		#endregion

		#region IPersistenceEngine Members
		public IDbConnection GetConnection()
		{
			return GetSQLiteConnection();
		}

		public IDataReader GetDataReader(DataTable table, Guid id)
		{
			SQLiteCommand command = GetSelectCommand(table, id);
			if (command != null)
			{

				return command.ExecuteReader(CommandBehavior.CloseConnection);
			}
			return null;
		}

		public void CreateTable(DataTable table)
		{
			if (table != null && table.PrimaryKey.Length > 0)
			{
				const string tableFormat = "CREATE TABLE IF NOT EXISTS {0} ({1})";
				const string columnFormat = "{0} {1} NOT NULL";
				StringBuilder columns = new StringBuilder();
				for (int i = 0; i < table.Columns.Count; i++)
				{
					if (i > 0) columns.Append(", ");
					columns.AppendFormat(columnFormat, table.Columns[i].ColumnName, GetColumnTypeName(table.Columns[i].DataType));
					if (table.PrimaryKey[0] == table.Columns[i])
						columns.Append(" PRIMARY KEY");
					else if (table.Columns[i].Unique)
						columns.Append(" UNIQUE");
				}
				string commandText = string.Format(tableFormat, table.TableName, columns);

				using (SQLiteConnection connection = GetSQLiteConnection())
				{
					SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
					connection.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void FillTable(DataTable dataTable, Guid id)
		{
			FillTable(dataTable, id, null);
		}

		public void FillTable(DataTable dataTable, string filter)
		{
			FillTable(dataTable, default(Guid), filter);
		}

		public void SaveRow(DataRow row)
		{
			SQLiteCommand cmd = new SQLiteCommand();

			string format = "REPLACE INTO {0} ({1}) VALUES ({2})";

			StringBuilder columns = new StringBuilder();
			StringBuilder values = new StringBuilder();

			for (int i = 0; i < row.Table.Columns.Count; i++)
			{
				if (i > 0)
				{
					columns.Append(", ");
					values.Append(", ");
				}

				string paramName = string.Format("@{0}", i);
				DataColumn col = row.Table.Columns[i];
				columns.Append(col.ColumnName);
				values.Append(paramName);

				cmd.Parameters.Add(new SQLiteParameter(paramName, GetValue(row, i)));
			}

			cmd.CommandText = string.Format(format, row.Table.TableName, columns, values);

			using (SQLiteConnection connection = GetSQLiteConnection())
			{
				connection.Open();
				cmd.Connection = connection;
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteRow(DataRow row)
		{
			string format = "DELETE FROM {0} WHERE {1} = '{2}'";
			string idColumn = row.Table.PrimaryKey[0].ColumnName;
			string commandText = string.Format(format, row.Table.TableName, idColumn, row[idColumn]);

			using (SQLiteConnection connection = GetSQLiteConnection())
			{
				connection.Open();
				SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
				cmd.ExecuteNonQuery();
			}
		}
		#endregion
	}
}