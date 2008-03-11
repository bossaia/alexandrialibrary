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
						databaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Alexandria");
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

		private string GetAffinity(Type type)
		{
			if (type != null)
			{
				string affinity = "TEXT";

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
					affinity = "INTEGER";

				if (type == typeof(float) ||
					type == typeof(double) ||
					type == typeof(decimal))
					affinity = "REAL";

				return affinity;
			}
			
			return null;
		}

		protected override void CreateEntityTables(Entity entity, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			const string tableFormat = "CREATE TABLE IF NOT EXISTS {0} ({1})";
			const string columnFormat = "{0} {1}";
			const string comma = ", ";
			
			StringBuilder createTableText = new StringBuilder();
			for (int count = 0; count < entity.Fields.Count; count++)
			{
				Field field = entity.Fields[count];
				if (count > 0) createTableText.Append(comma);
				createTableText.AppendFormat(columnFormat, field.Name, GetAffinity(field.Type));
				if (field == entity.Identifier)
				{
					createTableText.Append(" PRIMARY KEY NOT NULL");
				}
				else
				{
					if (field.IsUnique)
						createTableText.Append(" UNIQUE");
					if (field.IsRequired)
						createTableText.Append(" NOT NULL");
				}
			}

			foreach (Association association in entity.Associations)
			{
				const string assocTableFormat = "CREATE TABLE IF NOT EXISTS {0} ({1} {2} NOT NULL, {3} {4} NOT NULL, {5} {6} NOT NULL, PRIMARY KEY({1}, {3}))";
				string assocCommandText = string.Format(assocTableFormat, association.Name, association.ParentFieldName, GetAffinity(association.Parent.Identifier.Type), association.ChildFieldName, GetAffinity(association.Child.Identifier.Type), association.DateModifiedFieldName, GetAffinity(typeof(DateTime)));
				SQLiteCommand assocCommand = GetCommand(connection, transaction, assocCommandText);
				assocCommand.ExecuteNonQuery();
			}

			string commandText = string.Format(tableFormat, entity.Name, createTableText);
			SQLiteCommand command = GetCommand(connection, transaction, commandText);
			command.ExecuteNonQuery();
			
			foreach (Field field in entity.Fields)
			{
				if (!field.IsUnique && !field.IsHidden)
				{
					const string indexFormat = "CREATE INDEX IF NOT EXISTS index_{0} ON {1} ({0})";
					string createIndexText = string.Format(indexFormat, field.Name, entity.Name);
					SQLiteCommand createIndex = GetCommand(connection, transaction, createIndexText);
					createIndex.ExecuteNonQuery();
				}
			}
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
