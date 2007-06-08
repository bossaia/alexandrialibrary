using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using Alexandria;

namespace Alexandria.SQLite
{
	public class SQLiteDataProvider : IStorageEngine
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
		
		#region IsValueType
		/// <summary>
		/// Get a value indicating whether or not the given type is a value type
		/// </summary>
		/// <param name="type">The type in question</param>
		/// <returns>True if the type is a value type, false otherwise</returns>
		/// <remarks>Note - technically string is not a value type it is a primitive type but this is an expedient</remarks>
		private bool IsValueType(Type type)
		{
			bool isValueType = false;

			if (type == typeof(char) ||
				type == typeof(decimal) ||
				type == typeof(float) ||
				type == typeof(double) ||
				type == typeof(bool) ||
				type == typeof(byte) ||
				type == typeof(sbyte) ||
				type == typeof(ushort) ||
				type == typeof(short) ||
				type == typeof(uint) ||
				type == typeof(int) ||
				type == typeof(ulong) ||
				type == typeof(long) ||
				type == typeof(DateTime) ||
				type == typeof(TimeSpan))
			isValueType = true;
			
			return isValueType;
		}
		#endregion
		
		#region IsCollection
		private bool IsCollection(Type type)
		{
			//if (type.GetInterface("System.Collections.ICollection") != null)
				//return true;
			//else
			if (type.GetInterface("System.Collections.Generic.ICollection<T>") != null)
				return true;
			else
				return false;
		}
		#endregion
		
		#region DetermineTablesFromType
		private void DetermineTablesFromType(IList<DataTable> tables, Type type)
		{
			DataTable table = new DataTable(type.Name);
			
			foreach(PropertyInfo property in type.GetProperties(BindingFlags.GetProperty|BindingFlags.Public|BindingFlags.Instance))
			{
				if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
				{
					// Value types and strings are automatically included
					table.Columns.Add(property.Name, property.PropertyType);
				}
				else
				{
					if (IsCollection(property.PropertyType))
					{
						// Handle the collection
					}
					else
					{
						DetermineTablesFromType(tables, property.PropertyType);
					}
				}
			}
			
			tables.Add(table);
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
					if (count > 0) delimiter = ",";
					
					string columnType = string.Empty;
					string columnConstraint = string.Empty;
					
					if (string.Compare(column.ColumnName, "ID", true) == 0)
						columnConstraint += " PRIMARY KEY";
					 
					if (column.DataType == typeof(decimal) ||
						column.DataType == typeof(string))
					{
						columnType = "TEXT";
					}
					else 
					if (column.DataType == typeof(float) ||
						column.DataType == typeof(double))
					{
						columnType = "REAL";
						//columnConstraint += " NOT NULL";
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
						//columnConstraint += " NOT NULL";
					}
					
					sql.AppendFormat("{0}{1} {2} {3}", delimiter, column.ColumnName, columnType, columnConstraint);
					
					count++;
				}
				sql.Append(")");
				
				SQLiteConnection connection = GetSQLiteConnection(GetConnectionString());
				IDbCommand command = new SQLiteCommand(sql.ToString(), connection);
				int result = command.ExecuteNonQuery();
			}
			else throw new ArgumentNullException("table");
			
			return tableCreated;
		}
		#endregion
		
		#region GetChildCollectionByParentId
		private IList<IPersistant> GetChildCollectionByParentId(Guid id)
		{
			return new List<IPersistant>();
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
		
		/*
		#region Lookup
		public T Lookup<T>(IIdentifier id) where T: IMetadata
		{
			IList<DataTable> tables = new List<DataTable>();
			DetermineTablesFromType(tables, typeof(T));
			//if (TableExists)...
		
			return default(T);
		}
		
		public T Lookup<T>(Guid alexandriaId) where T: IMetadata
		{
			Type type = typeof(T);
		
			return default(T);
		}
		#endregion
		
		#region Save
		public bool Save<T>(T row) where T: IMetadata
		{
			bool saved = false;
			
			return saved;
		}
		#endregion
		
		#region Delete
		public bool Delete<T>(T row) where T: IMetadata
		{
			bool deleted = false;
			
			return deleted;
		}
		#endregion
		*/
		#endregion

		#region IStorageEngine Members
		public T Lookup<T>(Guid id) where T : IPersistant
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Save(IPersistant record)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Delete(IPersistant record)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
