#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
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
using System.Linq;
using System.Text;

using Telesophy.Babel.Persistence;

namespace Telesophy.Babel.Persistence.SQLite
{
	public class SQLiteEngine : EngineBase
		<SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter>
	{
		#region Constructors
		public SQLiteEngine() : base("SQLite Database Engine")
		{
		}
		#endregion
	
		#region Private Constants
		private const string CONFIG_DATABASE_DIR = "DatabaseDirectory";
		private const string FORMAT_CONNECTION_STRING = "Data Source={0};New={1};UTF8Encoding=True;Version=3";
		private const string DATABASE_FILE_EXT = ".db";
		#endregion
	
		#region Private Fields
		private string databaseDirectory;
		#endregion
	
		#region Protected Properties
		protected override string DatabaseDirectory
		{
			get {
				if (string.IsNullOrEmpty(databaseDirectory))
				{
					databaseDirectory = ConfigurationManager.AppSettings[CONFIG_DATABASE_DIR];
					if (string.IsNullOrEmpty(databaseDirectory))
					{
						databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					}
				}
				
				return databaseDirectory;
			}
		}
		#endregion
	
		#region Protected Methods
		protected override string GetConnectionString(Schema schema)
		{
			if (schema != null)
			{
				string dataSourcePath = Path.Combine(DatabaseDirectory, schema.Name + DATABASE_FILE_EXT);
				bool databaseIsNew = !File.Exists(dataSourcePath);
				return string.Format(FORMAT_CONNECTION_STRING, dataSourcePath, databaseIsNew);
			}
			
			return null;
		}

		protected override SQLiteConnection GetConnection(string connectionString)
		{
			return new SQLiteConnection(connectionString);
		}

		protected override SQLiteTransaction GetTransaction(SQLiteConnection connection)
		{
			if (connection != null)
			{
				//obtain a writer-lock immediately
				return connection.BeginTransaction(false);
			}
			
			return null;
		}

		protected override SQLiteCommand GetCommand(SQLiteConnection connection, SQLiteTransaction transaction, string commandText)
		{
			return new SQLiteCommand(commandText, connection, transaction);
		}

		protected override SQLiteCommand GetCommand(SQLiteConnection connection, SQLiteTransaction transaction, string commandText, IList<SQLiteParameter> parameters)
		{
			SQLiteCommand command = GetCommand(connection, transaction, commandText);
			if (parameters != null && parameters.Count > 0)
			{
				foreach (SQLiteParameter parameter in parameters)
					command.Parameters.Add(parameter);
			}
			
			return command;
		}

		protected override void CreateEntityTables(Entity entity, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			StringBuilder sql = new StringBuilder();

			SQLiteCommand command = GetCommand(connection, transaction, sql.ToString());
			command.ExecuteNonQuery();
		}

		protected override SQLiteCommand GetSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction, Entity entity, IExpression filter)
		{
			throw new NotImplementedException();
		}

		protected override SQLiteCommand GetSelectCommand(SQLiteConnection connection, SQLiteTransaction transaction, Map map, IExpression filter)
		{
			throw new NotImplementedException();
		}

		protected override SQLiteParameter GetParameter(string name, object value)
		{
			return new SQLiteParameter(name, value);
		}
		#endregion

		#region Public Methods
		public override void Save<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			throw new NotImplementedException();
		}

		public override void Delete<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
