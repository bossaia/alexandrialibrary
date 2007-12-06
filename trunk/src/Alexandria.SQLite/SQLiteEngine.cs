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
		
		private string GetColumnType(Type type)
		{
			if (type != null)
			{
				switch(type.Name)
				{
					case "String":
						return "TEXT";
					case "Short":
					case "Int":
					case "Long":
						return "INT";
					case "Decimal":
					case "Float":
					case "Double":
						return "NUMBER";
					case "TimeSpan":
						return "INT";
					case "DateTime":
						return "INT";
					default:
						return "TEXT";
				}
			}
			
			return string.Empty;
		}
		#endregion

		#region IPersistenceEngine Members
		public IDbConnection GetConnection()
		{
			return GetSQLiteConnection();
		}
		
		public IDataReader GetDataReader(string commandText)
		{
			SQLiteCommand command = new SQLiteCommand(commandText, GetSQLiteConnection());
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}
		
		public void CreateTable(DataTable table)
		{
			if (table != null)
			{
				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("CREATE TABLE {0} (", table.TableName);
				
			}
		}
		
		public void FillRow(DataRow row, Guid id)
		{
		}
		
		public void SaveRow(DataRow row)
		{
		}
		
		public void DeleteRow(DataRow row)
		{
		}
		#endregion
	}	
}
