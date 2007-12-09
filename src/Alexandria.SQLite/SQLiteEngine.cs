using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

using Alexandria.Persistence;

namespace Alexandria.SQLite
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
				
		private string GetValueName(DataRow row, int index)
		{
			if (row != null && index >= 0 && index < row.Table.Columns.Count)
			{
				DataColumn col = row.Table.Columns[index];
				string typeName = GetColumnTypeName(col.DataType);
				
				switch(typeName)
				{
					case "TEXT":
						return string.Format("'{0}'", row[index].ToString());
					case "INTEGER":
						if (col.DataType == typeof(DateTime))
							return string.Format("{0}", ((DateTime)row[index]).ToFileTime());
						else if (col.DataType == typeof(TimeSpan))
							return string.Format("{0}", ((TimeSpan)row[index]).Ticks);
						else return string.Format("{0}", row[index]);
					case "REAL":
						return string.Format("{0}", row[index]);
					default:
						break;
				}
			}
			return "''";
		}
		
		private SQLiteCommand GetSelectCommand(DataTable table, Guid id)
		{
			if (table != null && table.PrimaryKey.Length > 0)
			{
				string idField = table.PrimaryKey[0].ColumnName;
				string commandText = string.Format("SELECT * FROM {0} WHERE {1} = @Id", table.TableName, idField);
				SQLiteCommand command = new SQLiteCommand(commandText, GetSQLiteConnection());
				command.Parameters.Add(new SQLiteParameter("@Id", id.ToString()));
				return command;
			}
			return null;
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
		
		//public IDataReader GetDataReader(string commandText)
		//{
		//	SQLiteCommand command = new SQLiteCommand(commandText, GetSQLiteConnection());
		//	return command.ExecuteReader(CommandBehavior.CloseConnection);
		//}
		
		public void CreateTable(DataTable table)
		{
			if (table != null && table.PrimaryKey.Length > 0)
			{
				const string tableFormat = "CREATE TABLE IF NOT EXISTS {0} ({1})";
				const string columnFormat = "{0} {1} NOT NULL";
				StringBuilder columns = new StringBuilder();
				for(int i=0; i<table.Columns.Count; i++)
				{
					if (i > 0) columns.Append(", ");
					columns.AppendFormat(columnFormat, table.Columns[i].ColumnName, GetColumnTypeName(table.Columns[i].DataType));
					if (table.PrimaryKey[0] == table.Columns[i])
						columns.Append(" PRIMARY KEY");
					else if (table.Columns[i].Unique)
						columns.Append(" UNIQUE");
				}				
				string commandText = string.Format(tableFormat, table.TableName, columns);
				
				using(SQLiteConnection connection = GetSQLiteConnection())
				{
					SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
					connection.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}
		
		public void FillTable(DataTable dataTable, Guid id)
		{
			if (dataTable != null)
			{
				dataTable.Rows.Clear();
				SQLiteCommand cmd = GetSelectCommand(dataTable, id);
				if (cmd != null)
				{
					SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
					adapter.Fill(dataTable);
				}
			}
		}
		
		public void SaveRow(DataRow row)
		{
			string format = "REPLACE INTO {0} ({1}) VALUES ({2})";
						
			StringBuilder columns = new StringBuilder();
			StringBuilder values = new StringBuilder();
			
			for(int i=0; i<row.Table.Columns.Count; i++)
			{
				if (i > 0)
				{
					columns.Append(", ");
					values.Append(", ");
				}
			
				DataColumn col = row.Table.Columns[i];
				columns.Append(col.ColumnName);
				values.Append(GetValueName(row, i));
			}
			
			string commandText = string.Format(format, row.Table.TableName, columns, values);
			
			using(SQLiteConnection connection = GetSQLiteConnection())
			{
				connection.Open();
				SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
				cmd.ExecuteNonQuery();
			}
		}
		
		public void DeleteRow(DataRow row)
		{
			string format = "DELETE FROM {0} WHERE {1} = '{2}'";
			string idColumn = row.Table.PrimaryKey[0].ColumnName;
			string commandText = string.Format(format, row.Table.TableName, idColumn, row[idColumn]);
			
			using(SQLiteConnection connection = GetSQLiteConnection())
			{
				connection.Open();
				SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
				cmd.ExecuteNonQuery();
			}
		}
		#endregion
	}	
}
