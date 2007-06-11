#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using Alexandria;
using Alexandria.Data;

namespace Alexandria.SQLite
{
	public class SQLiteDataProvider : IDataStore
	{			
		#region Constructors
		public SQLiteDataProvider(string databasePath)
		{		
			this.databasePath = databasePath;
		}
		#endregion
		
		#region Private Constants
		private const string SCHEMA_TABLES = "Tables";
		private const string SCHEMA_TABLE_NAME = "TABLE_NAME";
		#endregion
		
		#region Private Fields
		string databasePath;
		#endregion
		
		#region Private Methods
		
		#region GetConnectionString
		private string GetConnectionString()
		{
			bool databaseIsNew = false;
			databaseIsNew = (!File.Exists(databasePath));
			return string.Format("Data Source={0};New={1};UTF8Encoding=True;Version=3", databasePath, databaseIsNew);		
		}
		#endregion
		
		#region GetSQLiteConnection
		private SQLiteConnection GetSQLiteConnection()
		{
			return GetSQLiteConnection(GetConnectionString());
		}
		
		private SQLiteConnection GetSQLiteConnection(string connectionString)
		{
			return new SQLiteConnection(connectionString);
		}
		#endregion

		#region TableExists
		private bool TableExists(string tableName)
		{
			SQLiteConnection connection = GetSQLiteConnection(GetConnectionString());
			DataTable tables = connection.GetSchema(SCHEMA_TABLES);
			foreach (DataRow tableInfo in tables.Rows)
			{
				if (string.Compare(tableInfo[SCHEMA_TABLE_NAME].ToString(), tableName, true) == 0)
					return true;
			}
			return false;
		}

		private bool TableExists<T>(T table) where T : IMetadata
		{
			if (table != null)
			{
				string tableName = table.GetType().Name;
				return TableExists(tableName);
			}
			else throw new ArgumentNullException("table");
		}
		#endregion				
				
		#region InstantiateType
		private T InstantiateType<T>(DataTable table) where T: IPersistant
		{
			Type type = typeof(T);
			ConstructorInfo ctor = null;
			
			foreach(ConstructorInfo constructor in type.GetConstructors(BindingFlags.NonPublic|BindingFlags.Public))
			{
				foreach (PersistanceConstructorAttribute attribute in constructor.GetCustomAttributes(typeof(PersistanceConstructorAttribute), false))
				{
					ctor = constructor;					
					break;					
				}
				
				if (ctor != null)
					break;
			}
			
			return default(T);
		}
		#endregion
		
		#region InstantiateCollection
		private IList<T> InstantiateCollection<T>(DataTable table) where T: IPersistant
		{
			return new List<T>();
		}
		#endregion
		
		#region NormalizeValue
		private object NormalizeValue(Type type, object value)
		{
			if (type == typeof(int))
			{
				if (value == DBNull.Value)
					return 0;
			}
			if (type == typeof(DateTime))
			{
				if (value == DBNull.Value)
					return DateTime.MinValue;
			}
			else if (type == typeof(TimeSpan))
			{
				if (value == null || value == DBNull.Value)
					return TimeSpan.Zero;
			}
			
			return value;
		}
		#endregion
		
		#region GetTablesFromType
		private void GetTablesFromType(IList<DataTable> tables, Type type)
		{
			PersistanceClassAttribute classAttribute = null;			
						
			foreach(PersistanceClassAttribute attribute in type.GetCustomAttributes(typeof(PersistanceClassAttribute), false))
			{
				classAttribute = attribute;
				break;
			}

			if (classAttribute != null)
			{
				string tableName = classAttribute.TableName;

				if (!string.IsNullOrEmpty(tableName))
				{
					DataTable table = new DataTable(tableName);
					IDictionary<int, DataColumn> columns = new Dictionary<int, DataColumn>();				
					int i = 1; int ordinal;
					
					foreach(PropertyInfo property in type.GetProperties(BindingFlags.GetProperty|BindingFlags.Public|BindingFlags.Instance))
					{
						foreach (PersistancePropertyAttribute attribute in property.GetCustomAttributes(typeof(PersistancePropertyAttribute), false))
						{
							if (attribute.FieldType == PersistanceFieldType.Basic)
							{
								ordinal = (attribute.Ordinal > 0) ? attribute.Ordinal : i;
								DataColumn column = new DataColumn(property.Name, property.PropertyType);
								column.Unique = attribute.IsUnique;
								column.AllowDBNull = !attribute.IsRequired;
								column.DefaultValue = attribute.DefaultValue;
								if (attribute.MaxLength > 0)																
									column.MaxLength = attribute.MaxLength;
								
								columns.Add(ordinal, column);
								i++;
							}
							else
							{
								if (attribute.FieldType == PersistanceFieldType.OneToOneChild)
								{
								}
								else if (attribute.FieldType == PersistanceFieldType.OneToManyChildren)
								{
									if (attribute.ChildType != null)
										GetTablesFromType(tables, attribute.ChildType);
								}							
							}
						}
					}
					
					if (columns.Count > 0)
					{
						for(int j=1;j<=columns.Count;j++)
							table.Columns.Add(columns[j]);
						
						tables.Add(table);
					}
					else throw new ApplicationException("Could not find any columns for type: " + type.Name);
				}
				else throw new ApplicationException("Could not determine the table name for type: " + type.Name);
			}
		}
		#endregion
		
		#region CreateTable
		private bool CreateTable(DataTable table)
		{
			bool tableCreated = false;
		
			if (table != null)
			{
				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("CREATE TABLE IF NOT EXISTS {0} (", table.TableName);
				
				int count = 0;
				foreach(DataColumn column in table.Columns)
				{
					string delimiter = string.Empty;
					if (count > 0) delimiter = ", ";
					
					string columnType = "TEXT";
					string columnConstraint = string.Empty;
					
					if (column.Unique)					
						columnConstraint += " UNIQUE";
					 
					 if (!column.AllowDBNull)
						columnConstraint += " NOT NULL";
					 
					if (column.DataType == typeof(decimal) ||
						column.DataType == typeof(string) ||
						column.DataType == typeof(Guid))
					{
						columnType = "TEXT";
					}
					else 
					if (column.DataType == typeof(float) ||
						column.DataType == typeof(double))
					{
						columnType = "REAL";
					}
					else
					if (column.DataType == typeof(bool) ||
						column.DataType == typeof(byte) ||
						column.DataType == typeof(sbyte) ||
						column.DataType == typeof(ushort) ||
						column.DataType == typeof(short) ||
						column.DataType == typeof(uint) ||
						column.DataType == typeof(int) ||
						column.DataType == typeof(ulong) ||
						column.DataType == typeof(long) ||
						column.DataType == typeof(DateTime) ||
						column.DataType == typeof(TimeSpan))
					{
						columnType = "INTEGER";						
					}
					
					sql.AppendFormat("{0}{1} {2}{3}", delimiter, column.ColumnName, columnType, columnConstraint);
					
					count++;
				}
				sql.Append(")");
				
				using (SQLiteConnection connection = GetSQLiteConnection(GetConnectionString()))
				{
					connection.Open();
					IDbCommand command = new SQLiteCommand(sql.ToString(), connection);
					tableCreated = (command.ExecuteNonQuery() == 0);
				}				
			}
			else throw new ArgumentNullException("table");
			
			return tableCreated;
		}
		#endregion
		
		#region LookupTable
		private void LookupTable(DataTable table, string idFieldName, Guid id)
		{
			if (table != null)
			{
				string selectFormat = "SELECT {0} FROM {1} WHERE {2} = '{3}'";
				StringBuilder selectList = new StringBuilder();
				const string PREFIX = ", ";
				foreach(DataColumn column in table.Columns)
				{				
					if (column.Ordinal > 0)
						selectList.AppendFormat("{0}{1}", PREFIX, column.ColumnName);
					else selectList.Append(column.ColumnName);
				}
				string sql = string.Format(selectFormat, selectList.ToString(), table.TableName, idFieldName, id.ToString().ToUpperInvariant());
				
				using (SQLiteConnection connection = GetSQLiteConnection())
				{
					connection.Open();
					SQLiteCommand command = new SQLiteCommand(sql, connection);
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						if (reader != null && reader.HasRows)
						{
							object[] data = new object[reader.FieldCount];
							reader.Read();
							int i = 0;
							foreach(DataColumn column in table.Columns)
							{
								data[i] = NormalizeValue(column.DataType, reader[column.ColumnName]);
								i++;
							}							
							table.Rows.Add(data);
						}
					}
				}
			}
		}
		#endregion
		
		#region GetChildCollectionByParentId
		private IList<IPersistant> GetChildCollectionByParentId(Guid id)
		{
			return new List<IPersistant>();
		}
		#endregion
		
		#region GetRecordById
		private T GetRecordByParentID<T>(Guid id) where T: IPersistant
		{
			return default(T);
		}
		
		private T GetRecordById<T>(Guid id) where T: IPersistant
		{
			try
			{
				IList<DataTable> tables = new List<DataTable>();
				GetTablesFromType(tables, typeof(T)); 
				
				if (tables.Count > 0)
				{
					DataTable mainTable = tables[tables.Count-1];
					LookupTable(mainTable, "Id", id);
					string x = mainTable.TableName;
					
					for(int i=0;i<tables.Count-1;i++)
					{
						LookupTable(tables[i], "ParentId", id);
					}
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Lookup error", ex);
			}
			
			return default(T);
		}
		#endregion
		
		#endregion
	
		#region Public Methods

		#region GetConnection
		public IDbConnection GetConnection()
		{
			return GetSQLiteConnection(GetConnectionString());
		}
		#endregion
		
		#region Test
		public string Test()
		{
			try
			{
				using (SQLiteConnection connection = GetSQLiteConnection(GetConnectionString()))
				{
					//connection.Open();
					//SQLiteCommand createCommand = new SQLiteCommand("CREATE TABLE AudioTrack (Id TEXT PRIMARY KEY, Location TEXT, Name TEXT, Album TEXT, Artist TEXT, Duration INT, ReleaseDate INT, TrackNumber INT, Format TEXT)", connection);
					//SQLiteCommand createCommand = new SQLiteCommand("CREATE TABLE MetadataId (Id TEXT PRIMARY KEY, ParentId TEXT, IdValue TEXT, IdType TEXT, IdVersion TEXT)", connection);
					//return createCommand.ExecuteNonQuery().ToString();
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("SQLite error", ex);
			}
			
			return string.Empty;
		}
		#endregion
		
		#endregion

		#region IDataStore Members
		public void Initialize(Type type)
		{
			IList<DataTable> tables = new List<DataTable>();
			GetTablesFromType(tables, type);
			foreach(DataTable table in tables)
				CreateTable(table);
		}
		
		public T Lookup<T>(Guid id) where T : IPersistant
		{
			T record = GetRecordById<T>(id);
			if (record != null)
				record.DataStore = this;
			
			return record;
		}

		public void Save(IPersistant record)
		{
		}

		public void Delete(IPersistant record)
		{
		}
		#endregion
	}
}
