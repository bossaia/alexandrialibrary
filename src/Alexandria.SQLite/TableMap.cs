using System;
using System.Data;
using System.Data.SQLite;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Alexandria.Data;

namespace Alexandria.SQLite
{
	internal class TableMap
	{
		#region Constructors
		public TableMap(SQLiteDataProvider provider, Type type, DataTable table, PersistanceClassAttribute classAttribute, ConstructorInfo constructor)
		{
			this.provider = provider;
			this.type = type;
			this.table = table;
			this.classAttribute = classAttribute;
			this.constructor = constructor;
		}
		#endregion

		#region Private Constants
		private const string SCHEMA_TABLES = "Tables";
		private const string SCHEMA_TABLE_NAME = "TABLE_NAME";
		#endregion
		
		#region Private Fields
		private SQLiteDataProvider provider;
		private DataTable table;
		private Type type;
		private PersistanceClassAttribute classAttribute;
		private ConstructorInfo constructor;
		private IDictionary<PropertyMap, TableMap> children = new Dictionary<PropertyMap, TableMap>();
		private bool isFilled;
		#endregion
		
		#region Private Methods
		
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

		#region TableExists
		internal bool TableExists(string tableName)
		{
			SQLiteConnection connection = provider.GetSQLiteConnection();
			DataTable tables = connection.GetSchema(SCHEMA_TABLES);
			foreach (DataRow tableInfo in tables.Rows)
			{
				if (string.Compare(tableInfo[SCHEMA_TABLE_NAME].ToString(), tableName, true) == 0)
					return true;
			}
			return false;
		}

		internal bool TableExists<T>(T table) where T : IMetadata
		{
			if (table != null)
			{
				string tableName = table.GetType().Name;
				return TableExists(tableName);
			}
			else throw new ArgumentNullException("table");
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
				foreach (DataColumn column in table.Columns)
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

				using (SQLiteConnection connection = provider.GetSQLiteConnection())
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
				foreach (DataColumn column in table.Columns)
				{
					if (column.Ordinal > 0)
						selectList.AppendFormat("{0}{1}", PREFIX, column.ColumnName);
					else selectList.Append(column.ColumnName);
				}
				string sql = string.Format(selectFormat, selectList.ToString(), table.TableName, idFieldName, id.ToString().ToUpperInvariant());

				using (SQLiteConnection connection = provider.GetSQLiteConnection())
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
								foreach (DataColumn column in table.Columns)
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

		#region FormatPascalCase
		private string FormatPascalCase(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (value.Length > 1)
				{
					return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1, value.Length-1);
				}
				else return value.ToUpperInvariant();
			}
			return value;
		}
		#endregion

		#region GetFactory
		private object GetFactory(Type type)
		{
			object factory = null;
			ConstructorInfo constructor = type.GetConstructor(new Type[]{});
			if (constructor != null)
				factory = constructor.Invoke(null);
			
			return factory;
		}
		#endregion

		#region GetFactoryMethod
		private MethodInfo GetFactoryMethod(TableMap map)
		{
			MethodInfo method = null;
		
			if (map.ClassAttribute.FactoryType != null)
			{
				if (!string.IsNullOrEmpty(map.ClassAttribute.FactoryMethodName))
				{
					Type factoryType = map.ClassAttribute.FactoryType;
					method = factoryType.GetMethod(map.ClassAttribute.FactoryMethodName);
				}				
			}			
			
			return method;
		}
		#endregion

		#region GetRecord
		private IPersistant GetRecord(TableMap map)
		{
			IPersistant record = null;
		
			if (map.ClassAttribute.LoadType == PersistanceLoadType.Constructor)
			{				
				if (map.Constructor != null)
					record = GetRecordFromConstructor(map.Constructor, map.Table.Rows[0]);
				else throw new ApplicationException("Lookup error: load constructor undefined");
			}
			if (ClassAttribute.LoadType == PersistanceLoadType.Property)
			{
				if (map.Constructor != null)
					record = (IPersistant)map.Constructor.Invoke(null);
				else throw new ApplicationException("Lookup error: load constructor undefined");
					
				LoadProperties(record, map, map.Table.Rows[0]);				
			}
			else if (ClassAttribute.LoadType == PersistanceLoadType.Factory)
			{
				object factory = GetFactory(ClassAttribute.FactoryType);
				if (factory != null)
				{
					MethodInfo method = GetFactoryMethod(map);
					if (method != null)
						record = GetRecordFromMethod(factory, method, map.Table.Rows[0]);
					else throw new ApplicationException("Lookup error: factory method undefined");
				}
				else throw new ApplicationException("Lookup error: could not create factory");
			}
			else throw new ApplicationException("Lookup error: invalid class load type");
			
			return record;
		}
		#endregion

		#region GetRecordFromConstructor
		private IPersistant GetRecordFromConstructor(ConstructorInfo constructor, DataRow row)
		{
			ParameterInfo[] info = constructor.GetParameters();
			object[] parameters = new object[info.Length];
		
			for(int i=0;i<info.Length;i++)
			{
				parameters[i] = row[FormatPascalCase(info[i].Name)];
			}
			
			IPersistant record = (IPersistant)Constructor.Invoke(parameters);
			
			return record;
		}
		#endregion				
		
		#region GetRecordFromMethod
		private IPersistant GetRecordFromMethod(object factory, MethodInfo method, DataRow row)
		{
			ParameterInfo[] info = method.GetParameters();
			object[] parameters = new object[info.Length];

			for (int i = 0; i < info.Length; i++)
			{
				parameters[i] = row[FormatPascalCase(info[i].Name)];
			}

			IPersistant record = (IPersistant)method.Invoke(factory, parameters);

			return record;
		}
		#endregion
		
		#region LoadProperties
		private void LoadProperties(IPersistant record, TableMap map, DataRow row)
		{
			foreach(PropertyInfo property in record.GetType().GetProperties(BindingFlags.Public))
			{
				property.SetValue(record, row[property.Name], null);
			}
		}
		#endregion
		
		#endregion
				
		#region Internal Properties
		internal Type Type
		{
			get { return type; }
		}
		
		internal DataTable Table
		{
			get { return table; }
		}
		
		internal PersistanceClassAttribute ClassAttribute
		{
			get { return classAttribute; }
		}
		
		internal ConstructorInfo Constructor
		{
			get { return constructor; }
		}
				
		internal IDictionary<PropertyMap, TableMap> Children
		{
			get { return children; }
		}
		
		internal bool IsFilled
		{
			get { return isFilled; }
		}
		#endregion
		
		#region Internal Methods
		
		#region CreateTables
		internal void CreateTables()
		{
			CreateTable(Table);
			foreach(TableMap childMap in Children.Values)
			{
				childMap.CreateTables();
			}
		}
		#endregion
		
		#region LookupRecord
		private IPersistant LookupRecord()
		{
			MethodInfo method = this.GetType().GetMethod("LookupRecordByMap", BindingFlags.NonPublic | BindingFlags.Instance);
			MethodInfo genericMethod = method.MakeGenericMethod(Type);

			return (IPersistant)genericMethod.Invoke(this, null);
		}

		internal T LookupRecord<T>(Guid id) where T : IPersistant
		{
			LookupTable(Table, "Id", id);
			IPersistant record = null;
									
			foreach (KeyValuePair<PropertyMap, TableMap> childPair in Children)
			{
				PropertyInfo property = childPair.Key.Property;
				PersistancePropertyAttribute attribute = childPair.Key.Attribute;
				if (childPair.Key.Attribute.FieldType == PersistanceFieldType.OneToOneChild)
				{
					IPersistant item = GetRecordFromConstructor(childPair.Value.Constructor, childPair.Value.Table.Rows[0]);
					childPair.Key.Property.SetValue(record, item, null);
				}
				else if (childPair.Key.Attribute.FieldType == PersistanceFieldType.OneToManyChildren)
				{
					LookupTable(childPair.Value.Table, "ParentId", id);					
					IList list = (IList)childPair.Key.Property.GetValue(record, null);
					
					foreach(DataRow row in childPair.Value.Table.Rows)
					{
						IPersistant item = GetRecordFromConstructor(childPair.Value.Constructor, row);
						list.Add(item);
					}
					int x = list.Count;
				}
			}

			record.DataStore = provider;
			return (T)record;
		}
		#endregion
		
		#endregion
	}
}
