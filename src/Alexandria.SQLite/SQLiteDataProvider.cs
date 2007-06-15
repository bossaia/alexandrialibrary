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
		private TableMapFactory mapFactory = new TableMapFactory();
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
				else return Convert.ToDateTime(value.ToString());
			}
			else if (type == typeof(TimeSpan))
			{
				if (value == null || value == DBNull.Value)
					return TimeSpan.Zero;
				else return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(value));
			}
			else if (type == typeof(Version) || type == typeof(IVersion))
			{
				return new Version(value.ToString());
			}
			else if (type == typeof(Location) || type == typeof(ILocation))
			{
				return new Location(value.ToString());
			}
			
			return value;
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
							while (reader.Read())
							{
								object[] data = new object[reader.FieldCount];
							
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
		}
		#endregion
		
		#region LookupChildMap
		private void LookupChildMap(TableMap map, Guid id, Guid parentId)
		{
			LookupTable(map.Table, "ParentId", parentId);
			foreach(TableMap childMap in map.Children.Values)
				LookupTable(childMap.Table, "ParentId", id);
		}
		#endregion
		
		#region GetRecordFromMapConstructor
		private IPersistant GetRecordFromMapConstructor(TableMap map)
		{
			return null;
		}
		#endregion
		
		#region LookupRecordByMap
		private IPersistant LookupRecordByMap(TableMap map)
		{
			MethodInfo method = this.GetType().GetMethod("LookupRecordByMap", BindingFlags.NonPublic | BindingFlags.Instance);
			MethodInfo genericMethod = method.MakeGenericMethod(map.Type);
			
			return (IPersistant)genericMethod.Invoke(this, new object[]{map});
		}
		
		private T LookupRecordByMap<T>(TableMap map, Guid id) where T: IPersistant
		{
			if (map != null)
			{
				LookupTable(map.Table, "Id", id);
				string x1 = map.Table.TableName;
				IDictionary<PropertyInfo, IPersistant> childOneToOneValues = new Dictionary<PropertyInfo, IPersistant>();
				IDictionary<PropertyInfo, IList<IPersistant>> childOneToManyValues = new Dictionary<PropertyInfo, IList<IPersistant>>();
				
				foreach(KeyValuePair<PropertyMap, TableMap> childPair in map.Children)
				{					
					PropertyInfo property = childPair.Key.Property;
					PersistancePropertyAttribute attribute = childPair.Key.Attribute;
					if (childPair.Key.Attribute.FieldType == PersistanceFieldType.OneToOneChild)
					{							
						// Property
						//IPersistant childRecord = LookupRecordByMap(childPair.Value);
						//childOneToOneValues.Add(childPair.Key.Property, childRecord);
					}
					else if (childPair.Key.Attribute.FieldType == PersistanceFieldType.OneToManyChildren)
					{
						// Collection
						LookupTable(childPair.Value.Table, "ParentId", id);
						string x2 = childPair.Value.Table.TableName;
						//IList<IPersistant> childRecords = LookupRecordCollectionByMap(childPair.Value);
						//childOneToManyValues.Add(childPair.Key.Property, childRecords);
					}
				}

				if (map.ClassAttribute.LoadType == PersistanceLoadType.Constructor)
				{
					// load with the constructor
					if (map.Constructor != null)
					{
						IPersistant record = GetRecordFromMapConstructor(map);
					}
					else throw new ApplicationException("Lookup error: load constructor undefined");
				}
				else if (map.ClassAttribute.LoadType == PersistanceLoadType.Property)
				{
					// load with properties
				}
				else if (map.ClassAttribute.LoadType == PersistanceLoadType.Factory)
				{
					// load with a factory
					Type factoryType = map.ClassAttribute.FactoryType;
				}
				else throw new ApplicationException("Lookup error: invalid class load type");
			}
			else throw new ApplicationException("Lookup error: table map undefined");
			
			return default(T);
		}
		#endregion
		
		#region LookupRecordCollectionByMap
		private IList<IPersistant> LookupRecordCollectionByMap(TableMap map)
		{
			return new List<IPersistant>();
		}
		#endregion
		
		#region CreateTablesFromMap
		private void CreateTablesFromMap(TableMap map)
		{
			CreateTable(map.Table);
			foreach(TableMap childMap in map.Children.Values)
				CreateTablesFromMap(childMap);
		}
		#endregion
		
		#region GetRecordFromDataRow
		private T GetRecordFromDataRow<T>(DataRow row)
		{
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
			TableMap map = mapFactory.CreateTableMap(type);
			CreateTablesFromMap(map);
		}
		
		public T Lookup<T>(Guid id) where T : IPersistant
		{
			TableMap map = mapFactory.CreateTableMap(typeof(T));
			T record = LookupRecordByMap<T>(map, id);
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
