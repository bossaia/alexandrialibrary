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

namespace Telesophy.Babel.Persistence.SQLite
{
	public class SQLiteEngine : IEngine
	{
		#region Constructors
		public SQLiteEngine()
		{
			if (ConfigurationManager.AppSettings[KEY_DB_DIR] != null)
				databaseDirectory = ConfigurationManager.AppSettings[KEY_DB_DIR].ToString();
			else databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + System.IO.Path.DirectorySeparatorChar + "Alexandria";

			if (!Directory.Exists(databaseDirectory))
				Directory.CreateDirectory(databaseDirectory);
		}
		#endregion
		
		#region Private Constants
		private const string KEY_DB_DIR = "DatabaseDirectory";
		private const string DATABASE_EXT = ".db";
		private const string LIST_SEPARATOR = ", ";
		private const string CON_STRING_FORMAT = "Data Source={0};New={1};UTF8Encoding=True;Version=3";
		private const string TABLE_FORMAT = "CREATE TABLE IF NOT EXISTS {0} ({1})";
		private const string COLUMN_FORMAT = "{0} {1} {2}";
		private const string SYSTEM_COLUMN_TEXT = LIST_SEPARATOR + USER_COLUMN_TEXT + LIST_SEPARATOR + DATE_COLUMN_TEXT;
		private const string INDEX_FORMAT = "CREATE INDEX IF NOT EXISTS index_{0} ON {1} ({0})";
		private const string VIEW_FORMAT = "CREATE VIEW IF NOT EXISTS view_{0} AS SELECT {1}";
		private const string ASSOCIATIVE_TABLE_TEXT = "ParentId NOT NULL, ChildId NOT NULL" + SYSTEM_COLUMN_TEXT + ", PRIMARY KEY(ParentId, ChildId)";
		private const string DATE_COLUMN_NAME = "_Date";
		private const string DATE_COLUMN_TEXT = DATE_COLUMN_NAME + " INTEGER NOT NULL";
		private const string USER_COLUMN_NAME = "_User";
		private const string USER_COLUMN_TEXT = USER_COLUMN_NAME + " TEXT NOT NULL";
		#endregion
			
		#region Private Fields
		private IDataConverter dataConverter = new DataConverter();
		private string databaseDirectory;
		#endregion
		
		#region Private Methods
		
		#region System.Data Methods
		private string GetConnectionString(ISchema schema)
		{
			if (schema != null)
			{
				string name = schema.Name + DATABASE_EXT;
				string path = Path.Combine(databaseDirectory, name);
				bool isNew = false;
				isNew = (!File.Exists(path));
				return string.Format(CON_STRING_FORMAT, path, isNew);
			}
			
			return null;
		}

		private SQLiteConnection GetConnection(ISchema schema)
		{
			return new SQLiteConnection(GetConnectionString(schema));
		}

		private SQLiteCommand GetCommand(string commandText, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			SQLiteCommand command = new SQLiteCommand(commandText, connection, transaction);
			command.CommandType = CommandType.Text;
			return command;
		}
		#endregion

		#region Initialization Methods
		private void InitializeMap(IMap map, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			if (map != null)
			{
				CreateEntityTable(map, connection, transaction);
				
				CreateIndices(map, connection, transaction);
			}
		}

		private void CreateEntityTable(IMap map, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			StringBuilder builder = new StringBuilder();
			
			Field field;
			for (int count = 0; count < map.Fields.Count; count++)
			{
				if (count > 0)
					builder.Append(LIST_SEPARATOR);

				field = map.Fields[count];
				builder.Append(GetColumnString(field));				
			}
			
			builder.Append(SYSTEM_COLUMN_TEXT);
			
			string commandText = string.Format(TABLE_FORMAT, map.Name, builder);
			SQLiteCommand command = GetCommand(commandText, connection, transaction);
			command.ExecuteNonQuery();
		}

		private void InitializeAssociations(IMap map, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			if (map != null)
			{
				foreach (Association association in map.Associations)
				{
					if (association != Association.Empty)
					{
						//switch (association.Function)
						//{
							//case AssociationFunction.ZeroOrOne:
							//case AssociationFunction.ZeroOrMore:
							//case AssociationFunction.One:
							//case AssociationFunction.OneOrMore:
							//case AssociationFunction.MoreThanOne:
								CreateAssociativeTable(association, connection, transaction); 
								CreateAssociativeView(association, connection, transaction);
								//break;
							//default:
								//break;
						//}
					}
				}
			}
		}
		
		private void CreateAssociativeTable(Association association, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			string commandText = string.Format(TABLE_FORMAT, association.Name, ASSOCIATIVE_TABLE_TEXT);
			SQLiteCommand command = GetCommand(commandText, connection, transaction);
			command.ExecuteNonQuery();
		}
		
		private void CreateAssociativeView(Association association, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			StringBuilder builder = new StringBuilder();
			
			builder.AppendFormat("{0}.ParentId {0}ParentId", association.Name);
			
			IMap childMap = association.Map.Schema.Maps[association.Type];
			foreach (Field field in childMap.Fields)
			{
				builder.AppendFormat(", {0}.{1} {1}", childMap.Name, field.Name);
			}
			
			builder.AppendFormat(" FROM {0} INNER JOIN {1} ON {0}.{2} = {1}.ParentId", association.Map.Name, association.Name, association.Map.Identifier.Name);
			builder.AppendFormat(" INNER JOIN {0} ON {1}.ChildId = {0}.{2}", childMap.Name, association.Name, childMap.Identifier.Name);
			
			string commandText = string.Format(VIEW_FORMAT, association.Name, builder);
			SQLiteCommand command = GetCommand(commandText, connection, transaction);
			command.ExecuteNonQuery();
		}
		
		private void CreateIndices(IMap map, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			if (map != null)
			{
				foreach (Field field in map.Fields)
				{
					switch (field.Function)
					{
						case FieldFunction.TypeDescriminator:
						case FieldFunction.Value:
							string commandText = string.Format(INDEX_FORMAT, field.Name, map.Name);
							SQLiteCommand command = GetCommand(commandText, connection, transaction);
							command.ExecuteNonQuery();
							break;
						default:
							break;
					}
				}
			}		
		}

		private string GetColumnString(Field field)
		{
			if (field != Field.Empty)
			{
				string name = field.Name;
				string affinity = GetColumnAffinity(field.Type);
				string constraints = GetColumnConstraints(field);
				return string.Format(COLUMN_FORMAT, name, affinity, constraints);
			}

			return string.Empty;
		}

		private string GetColumnAffinity(Type type)
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

		private string GetColumnConstraints(Field field)
		{
			if (field != Field.Empty)
			{
				switch (field.Function)
				{
					case FieldFunction.Identifier:
						return "PRIMARY KEY NOT NULL";
					case FieldFunction.TypeDescriminator:
					case FieldFunction.Value:
					case FieldFunction.NonIndexedValue:
						return "NOT NULL";
					case FieldFunction.UniqueValue:
						return "UNIQUE NOT NULL";
					default:
						break;
				}
			}

			return string.Empty;
		}
		#endregion

		#region Conversion Methods
		private object GetValue(DataRow row, int index)
		{
			if (row != null && index >= 0 && index < row.Table.Columns.Count)
			{
				DataColumn col = row.Table.Columns[index];
				string affinity = GetColumnAffinity(col.DataType);

				switch (affinity)
				{
					case "TEXT":
						if (row[index] != null && row[index] != DBNull.Value)
							return row[index].ToString();
						else return string.Empty;
					case "INTEGER":
						if (col.DataType == typeof(bool))
							return Convert.ToBoolean((int)row[index]);
						else if (col.DataType == typeof(DateTime))
							return ((DateTime)row[index]).ToFileTimeUtc();
						else if (col.DataType == typeof(TimeSpan))
							return ((TimeSpan)row[index]).Ticks;
						else
							return Convert.ToInt32(row[index]);
					case "REAL":
						return Convert.ToDecimal(row[index]);
					default:
						break;
				}
			}
			return string.Empty;
		}
		#endregion
		
		#endregion
		
		#region INamedItem Members
		public string Name
		{
			get { return "SQLite Database Engine"; }
		}
		#endregion
		
		#region Old IEngine Members
		public void Initialize(ISchema schema)
		{
			if (schema != null)
			{
				SQLiteConnection connection = null;
				SQLiteTransaction transaction = null;
				try
				{
					connection = GetConnection(schema);
					connection.Open();
					transaction = connection.BeginTransaction(false);
					
					foreach (IMap map in schema.Maps)
					{
						InitializeMap(map, connection, transaction);
					}
					
					//NOTE: this has to be a second pass because associations depend on maps being initialized first
					foreach (IMap map in schema.Maps)
					{
						InitializeAssociations(map, connection, transaction);
					}
					
					transaction.Commit();
				}
				catch (Exception ex)
				{
					if (transaction != null)
						transaction.Rollback();
					
					throw ex;
				}
				finally
				{
					if (connection != null && connection.State != ConnectionState.Closed)
						connection.Close();
				}
			}
		}

		public IEnumerable<Model> Lookup<Model>(IMap<Model> map, IQuery query)
		{
			throw new NotImplementedException();
		}

		public void Save<Model>(IMap<Model> map, DataSet dataSet)
		{
			if (map != null && dataSet != null)
			{
				//DataSet dataSet = map.GetDataSet(models);
			}
		}

		public void Delete<Model>(IMap<Model> map, DataSet dataSet)
		{
			if (map != null && dataSet != null)
			{
				//DataSet dataSet = map.GetDataSet(models);
			}
		}
		#endregion
		
		#region IEngine Members
		public IDataConverter DataConverter
		{
			get { return dataConverter; }
		}
		
		public void Initialize(DataSet dataSet)
		{
		}
		
		public DataSet Lookup(DataSet dataSet, IQuery query)
		{
			return null;
		}
		
		public void Save(DataSet dataSet)
		{
		}
		
		public void Delete(DataSet dataSet)
		{
		}
		#endregion
	}
}
