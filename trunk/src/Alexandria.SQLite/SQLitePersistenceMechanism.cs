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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using Alexandria;
using Alexandria.Persistence;

namespace Alexandria.SQLite
{
	public class SQLitePersistenceMechanism : IPersistenceMechanism
	{
		#region Constructors
		public SQLitePersistenceMechanism(string databasePath)
		{
			this.databasePath = databasePath;
		}
		#endregion

		#region Private Constant Fields
		private const string NULL_STRING = "NULL";
		private const string RECORD_TYPE_ID = "_RecordTypeId";
		#endregion

		#region Private Fields
		string databasePath;
		IPersistenceBroker broker;
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

		#region GetFactoryFieldName
		//NOTE: Should this be refactored into the FactoryMap?
		private string GetFactoryFieldName(string value) //, FieldMap fieldMap)
		{
			//HACK: We need to definitively match the parameter to the appropriate FieldMap
			//This is grasping at straws because it relies on the method parameters being in the same order as the database columns
			//if ((string.Compare(value, fieldMap.Property.Name, true) == 0) && !string.IsNullOrEmpty(fieldMap.Attribute.FieldName))
			//{
				//return fieldMap.Attribute.FieldName;
			//}
			//else 
			if (!string.IsNullOrEmpty(value) && value.Length > 1)
			{
				return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1, value.Length - 1);
			}
			else return value;
		}
		#endregion
		
		#region GetRecordValueFromDatabaseValue
		private object GetRecordValueFromDatabaseValue(Type type, object value)
		{	
			if (type == typeof(bool))
			{
				if (value  == DBNull.Value)
					return false;
				else return Convert.ToBoolean(value);
			}
			if (type == typeof(byte))
			{
				if (value == DBNull.Value)
					return 0;
				else return Convert.ToByte(value);
			}
			if (type == typeof(short))
			{
				if (value == DBNull.Value)
					return 0;
				else return Convert.ToInt16(value);
			}
			if (type == typeof(int))
			{
				if (value == DBNull.Value)
					return 0;
				else return Convert.ToInt32(value);
			}
			if (type == typeof(long))
			{
				if (value == DBNull.Value)
					return 0;
				else return Convert.ToInt64(value);
			}
			if (type == typeof(decimal))
			{
				if (value == DBNull.Value)
					return 0m;
				else return Convert.ToDecimal(value);
			}
			if (type == typeof(DateTime))
			{
				if (value == DBNull.Value)
					return DateTime.MinValue;
				else return DateTime.FromFileTime(Convert.ToInt64(value));
				//Convert.ToDateTime(value.ToString());
			}
			else if (type == typeof(TimeSpan))
			{
				if (value == null || value == DBNull.Value)
					return TimeSpan.Zero;
				else return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(value));
			}
			else if (type == typeof(Guid))
			{
				return new Guid(value.ToString());
			}
			else if (type == typeof(Version))
			{
				return new Version(value.ToString());
			}
			else if (type == typeof(Uri))
			{
				return new Uri(value.ToString());
			}

			return value;
		}
		#endregion

		#region GetSQLiteFieldValue
		private string GetSQLiteFieldValue(IRecord record, FieldMap fieldMap, object value)
		{
			if (fieldMap.Attribute.Type == FieldType.Basic)
			{
				Type type = fieldMap.Property.PropertyType;
			
				if (value == DBNull.Value || value == null)
					return NULL_STRING;

				else if (type == typeof(DateTime))
				{
					DateTime date = (DateTime)value;
					if (date == DateTime.MinValue)
						return NULL_STRING;
					else return date.ToFileTime().ToString();
				}
				else if (type == typeof(TimeSpan))
				{
					TimeSpan span = (TimeSpan)value;
					return span.TotalMilliseconds.ToString();
				}				
			}
			else
			{
				if (fieldMap.Attribute.Type == FieldType.Parent)
				{
					return string.Format("{0}", record.Id);
				}
				else if (fieldMap.Attribute.Type == FieldType.Child)
				{
					if (record.Parent != null)
						return string.Format("{0}", record.Parent.Id);
					else return NULL_STRING;
				}				
			}
			
			return value.ToString();
		}
		#endregion

		#region GetSQLiteFieldName
		private string GetSQLiteFieldName(FieldMap fieldMap)
		{
			string dbFieldName = string.Empty;
			
			if (!string.IsNullOrEmpty(fieldMap.Attribute.FieldName))
			{
				dbFieldName = fieldMap.Attribute.FieldName;
			}
			else
			{
				dbFieldName = fieldMap.Property.Name;	
			}
			
			return dbFieldName;
		}
		#endregion

		#region GetSQLiteFieldType
		private string GetSQLiteFieldType(Type type)
		{
			//integer, real, text
			string dbType = "TEXT";
			
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
			dbType = "INTEGER";
			
			if (type == typeof(float) ||
				type == typeof(double))
			dbType = "REAL";
			
			return dbType;
		}
		#endregion

		#region GetSQLiteTypeAffinity
		private TypeAffinity GetSQLiteTypeAffinity(string type)
		{
			switch (type)
			{
				case "INTEGER":
					return TypeAffinity.Int64;
				case "REAL":
					return TypeAffinity.Double;
				default:
					if (!string.IsNullOrEmpty(type))
						return TypeAffinity.Text;
					else return TypeAffinity.Uninitialized;
			}
		}
		
		private TypeAffinity GetSQLiteTypeAffinity(Type type)
		{
			return GetSQLiteTypeAffinity(GetSQLiteFieldType(type));
		}
		#endregion

		#region GetSQLiteFieldConstraints
		private string GetSQLiteFieldConstraints(FieldConstraints constraints)
		{
			StringBuilder dbConstraints = new StringBuilder(string.Empty);
			if ((constraints & FieldConstraints.Required) == FieldConstraints.Required) dbConstraints.Append(" NOT NULL");
			if ((constraints & FieldConstraints.Unique) == FieldConstraints.Unique) dbConstraints.Append(" UNIQUE");
			
			return dbConstraints.ToString();
		}
		#endregion

		#region GetSQLiteDataReader
		private SQLiteDataReader GetSQLiteDataReader(string linkRecordName, string linkParentField, string linkChildField, string childRecordName, string value, SQLiteConnection connection)
		{
			string commandText = string.Format("SELECT {0}.* FROM {1} INNER JOIN {0} ON {1}.{2} = {0}.Id WHERE {1}.{3} = @Id", childRecordName, linkRecordName, linkChildField, linkParentField);
			SQLiteCommand selectCommand = new SQLiteCommand(commandText, connection);
			selectCommand.Parameters.Add(new SQLiteParameter("@Id", value));
			return selectCommand.ExecuteReader();		
		}
		
		private SQLiteDataReader GetSQLiteDataReader(string recordName, string fieldName, string value, SQLiteConnection connection)
		{
			string commandText = string.Format("SELECT * FROM {0} WHERE {1} = @Id", recordName, fieldName);
			SQLiteCommand selectCommand = new SQLiteCommand(commandText, connection);
			selectCommand.Parameters.Add(new SQLiteParameter("@Id", value));
			return selectCommand.ExecuteReader();
		}
		#endregion

		#region GetIndexColumns
		private IList<ColumnInfo> GetIndexColumns(TableInfo table, string indexName, SQLiteConnection connection)
		{
			IList<ColumnInfo> columns = new List<ColumnInfo>();
			
			string commandText = string.Format("PRAGMA index_info('{0}')", indexName);
			SQLiteCommand command = new SQLiteCommand(commandText, connection);
			using (SQLiteDataReader reader = command.ExecuteReader())
			{
				if (reader != null && reader.HasRows)
				{
					while (reader.Read())
					{
						int columnIndex = reader.GetInt32(1) + 1;
						string columnName = reader[2].ToString();
						
						ColumnInfo column = table.GetColumnByName(columnName);
						if (column != default(ColumnInfo))
							columns.Add(column);
					}
				}
			}
			
			return columns;
		}
		#endregion

		#region GetIndexName
		private string GetIndexName(string recordName, string indexName)
		{
			return string.Format("index_{0}_{1}", recordName, indexName);
		}
		#endregion

		#region GetIndices
		private void GetIndices(TableInfo table, SQLiteConnection connection)
		{			
			string commandText = string.Format("PRAGMA index_list('{0}')", table.Name);
			SQLiteCommand command = new SQLiteCommand(commandText, connection);
			using (SQLiteDataReader reader = command.ExecuteReader())
			{
				if (reader != null && reader.HasRows)
				{
					while (reader.Read())
					{
						string name = reader[1].ToString();
						bool isUnique = Convert.ToBoolean(reader[2]);
						IList<ColumnInfo> columns = GetIndexColumns(table, name, connection);
						IndexInfo indexInfo = new IndexInfo(table, name, isUnique, columns);
						table.Indices.Add(name, indexInfo);
					}
				}
			}
		}
		
		private void GetIndices(TableInfo table, RecordMap recordMap)
		{
			Dictionary<string, bool> uniqueFlags = new Dictionary<string,bool>();
			Dictionary<string, Dictionary<int, ColumnInfo>> aggregateIndices = new Dictionary<string, Dictionary<int, ColumnInfo>>();
			
			foreach(IndexMap indexMap in recordMap.IndexMaps)
			{
				FieldMap fieldMap = recordMap.GetFieldMapByProperty(indexMap.Property);
				string indexName = GetIndexName(recordMap.Name, indexMap.Name);
			
				if (indexMap.Attribute.Ordinal == 0)
				{
					ColumnInfo column = table.GetColumnByName(fieldMap.Name);
					IList<ColumnInfo> columns = new List<ColumnInfo>();
					columns.Add(column);
					IndexInfo indexInfo = new IndexInfo(table, indexName, indexMap.IsUnique, columns);
					table.Indices.Add(fieldMap.Name, indexInfo);
				}
				else
				{
					if (!aggregateIndices.ContainsKey(indexName))
						aggregateIndices.Add(indexName, new Dictionary<int,ColumnInfo>());
					
					if (!uniqueFlags.ContainsKey(indexName))
						uniqueFlags.Add(indexName, indexMap.Attribute.IsUnique);
					
					ColumnInfo column = table.GetColumnByName(fieldMap.Name);
					Dictionary<int, ColumnInfo> indexColumns = aggregateIndices[indexName];
					indexColumns.Add(indexMap.Attribute.Ordinal, column);
				}				
			}
			
			foreach(string indexName in aggregateIndices.Keys)
			{
				IList<ColumnInfo> columns = new List<ColumnInfo>();
				Dictionary<int, ColumnInfo> columnMap = aggregateIndices[indexName];
				for(int i=1; i<=columnMap.Count; i++)
					columns.Add(columnMap[i]);
				bool isUnique = uniqueFlags[indexName];
				
				IndexInfo indexInfo = new IndexInfo(table, indexName, isUnique, columns);
				table.Indices.Add(indexName, indexInfo);
			}
		}
		#endregion

		#region GetTableInfo
		private TableInfo GetTableInfo(string recordName, SQLiteConnection connection)
		{
			TableInfo tableInfo = default(TableInfo);
		
			string commandText = string.Format("PRAGMA table_info({0})", recordName);
			SQLiteCommand command = new SQLiteCommand(commandText, connection);
			using (SQLiteDataReader reader = command.ExecuteReader())
			{
				if (reader.HasRows)
				{					
					IDictionary<int, ColumnInfo> columns = new Dictionary<int, ColumnInfo>();
				
					while(reader.Read())
					{
						int ordinal = reader.GetInt32(0) + 1; //SQLite it 0-based but RecordMap is 1-based
						string name = reader[1].ToString();
						TypeAffinity type = GetSQLiteTypeAffinity(reader[2].ToString());
						bool isRequired = (reader[3].ToString() == "99") ? true : false; //0 = NULL, 99 = NOT NULL
						object defaultValue = reader[4];
						if (defaultValue == DBNull.Value) defaultValue = null;
						bool isPrimaryKey = Convert.ToBoolean(reader[5]); //0 = NO, 1 = YES
						columns.Add(ordinal, new ColumnInfo(ordinal, name, type, isRequired, defaultValue, isPrimaryKey));
					}
					tableInfo = new TableInfo(recordName, columns);
				}
			}
			
			GetIndices(tableInfo, connection);
			
			return tableInfo;
		}
		
		private TableInfo GetTableInfo(RecordMap recordMap)
		{
			TableInfo tableInfo = default(TableInfo);
			if (recordMap != null)
			{
				IDictionary<int, ColumnInfo> columns = new Dictionary<int, ColumnInfo>();
			
				for(int ordinal=1; ordinal<=recordMap.BasicFieldMaps.Count; ordinal++)
				{
					string name = GetSQLiteFieldName(recordMap.BasicFieldMaps[ordinal]);
					TypeAffinity type = GetSQLiteTypeAffinity(recordMap.BasicFieldMaps[ordinal].Property.PropertyType);
					bool isRequired = ((recordMap.BasicFieldMaps[ordinal].Attribute.Constraints & FieldConstraints.Required) == FieldConstraints.Required);
					object defaultValue = recordMap.BasicFieldMaps[ordinal].Attribute.DefaultValue;
					bool isPrimaryKey = ((recordMap.BasicFieldMaps[ordinal].Attribute.Constraints & FieldConstraints.PrimaryKey) == FieldConstraints.PrimaryKey);
					columns.Add(ordinal, new ColumnInfo(ordinal, name, type, isRequired, defaultValue, isPrimaryKey));
				}
				
				//Add RecordTypeId column
				columns.Add(columns.Count+1, new ColumnInfo(columns.Count+1, RECORD_TYPE_ID, TypeAffinity.Text, true, null, false));
				
				tableInfo = new TableInfo(recordMap.RecordAttribute.Name, columns);
			}
			
			GetIndices(tableInfo, recordMap);
			
			return tableInfo;
		}
		#endregion

		#region IndicesHaveChanged
		private bool IndicesHaveChanged(TableInfo classTableInfo, TableInfo dbTableInfo)
		{
			if (classTableInfo.Indices != null)
			{
				if (dbTableInfo.Indices != null)
				{
					if (classTableInfo.Indices.Count == dbTableInfo.Indices.Count)
					{
						foreach (IndexInfo index in classTableInfo.Indices.Values)
						{
							if (dbTableInfo.Indices.ContainsKey(index.Name))
							{
								if (index != dbTableInfo.Indices[index.Name])
									return true;
							}
							else return true;
						}
					}
					else return true;
				}
				else return true;
			}
			else if (dbTableInfo.Indices != null)
				return true;
				
			return false;
		}
		#endregion

		#region InitializeParentRecordMap
		private void InitializeParentRecordMap(RecordMap recordMap, SQLiteTransaction transaction)
		{
			TableInfo dbTableInfo = GetTableInfo(recordMap.RecordAttribute.Name, transaction.Connection);
			TableInfo classTableInfo = GetTableInfo(recordMap);
			if (dbTableInfo != classTableInfo)
			{
				if (dbTableInfo == default(TableInfo))
				{
					SQLiteCommand createTable = new SQLiteCommand(classTableInfo.ToString(), transaction.Connection, transaction);				
					createTable.ExecuteNonQuery();
				}
				else
				{
					//Alter
					//1. create temp table
					//2. insert record table into temp table
					//3. drop record table
					//4. create record table
					//5. insert temp table into record table
					
					string tempName = "TEMP_" + classTableInfo.Name;
					string createTempCommandText = dbTableInfo.GetCreateTempTableCommandText(tempName);
					SQLiteCommand createTempCommand = new SQLiteCommand(createTempCommandText, transaction.Connection);
					//createTempCommand.ExecuteNonQuery();
					
					string firstInsertCommandText = dbTableInfo.GetInsertCommandText(tempName);
					SQLiteCommand firstInsertCommand = new SQLiteCommand(firstInsertCommandText, transaction.Connection);
					//firstInsertCommand.ExecuteNonQuery();
					
					string dropCommandText = string.Format("DROP TABLE IF EXISTS {0}", dbTableInfo.Name);
					SQLiteCommand dropCommand = new SQLiteCommand(dropCommandText, transaction.Connection);
					//dropCommand.ExecuteNonQuery();
					
					SQLiteCommand createTableCommand = new SQLiteCommand(classTableInfo.ToString(), transaction.Connection);
					string x = createTableCommand.CommandText;
					//createTableCommand.ExecuteNonQuery();
					
					//TODO: Account for temp table having fewer fields than class table
					//string secondInsertCommandText = classTableInfo.get
				}				
			}

			if (IndicesHaveChanged(classTableInfo, dbTableInfo))
			{
				//Remove non-unique database indices that are no longer defined in the class
				foreach (IndexInfo index in dbTableInfo.Indices.Values)
				{
					if (!classTableInfo.Indices.ContainsKey(index.Name))
					{
						//Unique indices cannot be dropped
						if (!index.IsUnique)
						{
							SQLiteCommand dropIndex = new SQLiteCommand(string.Format("DROP INDEX IF EXISTS {0}", index.Name), transaction.Connection);
							dropIndex.ExecuteNonQuery();
						}
					}
				}

				//Create any indices defined in the class that do not exist in the database
				foreach (IndexInfo index in classTableInfo.Indices.Values)
				{
					if (!dbTableInfo.Indices.ContainsKey(index.Name))
					{
						SQLiteCommand createIndex = new SQLiteCommand(index.ToString(), transaction.Connection, transaction);
						createIndex.ExecuteNonQuery();
					}
				}
			}
		}
		#endregion
		
		#region InitializeLinkedRecordMaps
		private void InitializeLinkedRecordMaps(RecordMap recordMap, DbTransaction transaction)
		{
			foreach (LinkRecord linkRecord in recordMap.LinkRecords)
			{
				if (linkRecord.FieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
				{
					if (linkRecord.FieldMap.Attribute.Type == FieldType.Parent)
					{
						string createLinkTableCommandText = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1} NOT NULL, {2} NOT NULL, UNIQUE ({1}, {2}))", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName);
						SQLiteCommand createLinkTable = new SQLiteCommand(createLinkTableCommandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
						createLinkTable.ExecuteNonQuery();

						string createLinkParentIndexCommandText = string.Format("CREATE INDEX IF NOT EXISTS index_{0}_{1} ON {0} ({1})", linkRecord.Name, linkRecord.ParentFieldName);
						SQLiteCommand createLinkParentIndex = new SQLiteCommand(createLinkParentIndexCommandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
						createLinkParentIndex.ExecuteNonQuery();
						
						//NOTE: this is already covered by the UNIQUE constraint in the CREATE TABLE command above
						//string createLinkIndexCommandText = string.Format("CREATE UNIQUE INDEX IF NOT EXISTS index_{0} ON {0} ({1}, {2})", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName);
						//SQLiteCommand createLinkIndex = new SQLiteCommand(createLinkIndexCommandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
						//createLinkIndex.ExecuteNonQuery();
					}
				}
			}
		}
		#endregion

		#region GetRecordFromDataReader
		private IRecord GetRecordFromDataReader(SQLiteDataReader reader, RecordMap recordMap)
		{
			ParameterInfo[] parameterInfo = recordMap.FactoryMap.GetParameters();
			object[] parameters = new object[parameterInfo.Length];
			for (int i = 0; i < parameterInfo.Length; i++)
			{
				string paramFieldName = GetFactoryFieldName(parameterInfo[i].Name); //, recordMap.BasicFieldMaps[i+1]);
				object paramValue = GetRecordValueFromDatabaseValue(parameterInfo[i].ParameterType, reader[paramFieldName]);
				parameters[i] = paramValue;
			}

			IRecord record = recordMap.FactoryMap.GetRecord(parameters);
			record.PersistenceBroker = broker;
			return record;
		}
		#endregion

		#region LookupRecords
		private IList<IRecord> LookupRecords(string linkRecordName, string linkParentField, string linkChildField, string childRecordName, string value, Type proxyType, SQLiteConnection connection)
		{
			IList<IRecord> records = new List<IRecord>();
			
			RecordMap recordMap = null;
			if (proxyType != null)
				recordMap = broker.ProxyRecordMaps[proxyType];
			
			using (SQLiteDataReader reader = GetSQLiteDataReader(linkRecordName, linkParentField, linkChildField, childRecordName, value, connection))
			{
				if (reader != null && reader.HasRows)
				{
					while(reader.Read())
					{
						if (proxyType == null)
							recordMap = broker.RecordMaps[reader[RECORD_TYPE_ID].ToString()];
					
						IRecord record = GetRecordFromDataReader(reader, recordMap);
						records.Add(record);
					}
				}
			}
			
			return records;
		}
		
		private IList<IRecord> LookupRecords(string recordName, string fieldName, string value, Type proxyType, SQLiteConnection connection)
		{
			IList<IRecord> records = new List<IRecord>();

			RecordMap recordMap = null;
			if (proxyType != null)
				recordMap = broker.ProxyRecordMaps[proxyType];
			
			using (SQLiteDataReader reader = GetSQLiteDataReader(recordName, fieldName, value, connection))
			{
				if (reader != null && reader.HasRows)
				{
					while(reader.Read())
					{
						if (proxyType == null)
							recordMap = broker.RecordMaps[reader[RECORD_TYPE_ID].ToString()];
						
						IRecord record = GetRecordFromDataReader(reader, recordMap);
						records.Add(record);
					}
				}
			}
			
			return records;
		}
		#endregion

		#region LookupParentRecord
		private IRecord LookupParentRecord(string recordName, string fieldName, string value, Type proxyType, SQLiteConnection connection)
		{
			IList<IRecord> records = LookupRecords(recordName, fieldName, value, proxyType, connection);
			if (records != null && records.Count > 0)
				return records[0];
			else return null;
		}
		#endregion
				
		#region LookupChildRecords
		private void LookupChildRecords(IRecord record, RecordMap recordMap, SQLiteConnection connection)
		{
			System.Diagnostics.Debug.WriteLine(string.Format("LookupChildRecords: {0}({1})", recordMap.RecordAttribute.Name, record.Id));
		
			foreach(FieldMap fieldMap in recordMap.AdvancedFieldMaps)
			{
				if (fieldMap.Attribute.Type == FieldType.Parent)
				{
					Type childType = fieldMap.Property.PropertyType;
					IList propertyList = fieldMap.Property.GetValue(record, null) as IList;
					if (propertyList != null)
						childType = fieldMap.Attribute.ChildType;
					
					RecordAttribute recordAttribute = broker.RecordAttributes[childType];
					if (recordAttribute != null)
					{
						IList<IRecord> childRecordList = GetChildRecordList(recordAttribute, fieldMap, record.Id.ToString(), connection);
						if (childRecordList != null && childRecordList.Count > 0)
						{
							if (propertyList != null)
							{
								foreach(IRecord childRecord in childRecordList)
								{
									childRecord.Parent = record;
									propertyList.Add(childRecord);
								}
							}
							else
							{
								IRecord childRecord = childRecordList[0];
								childRecord.Parent = record;
								fieldMap.Property.SetValue(record, childRecord, null);
							}
						}
					}
					else throw new ApplicationException("SQLite could not lookup record attribute for child record");
				}
			}
		}
		#endregion

		#region LookupLinkedRecords
		private IList<IRecord> LookupLinkedRecords(FieldMap fieldMap, string parentId, Type proxyType, SQLiteConnection connection)
		{
			System.Diagnostics.Debug.WriteLine(string.Format("LookupLinkedRecords: {0}({1}", fieldMap.Property.Name, parentId));
			IList<IRecord> linkedRecords = null;
		
			RecordAttribute recordAttribute = broker.RecordAttributes[fieldMap.Attribute.ChildType];
							
			linkedRecords = LookupRecords(fieldMap.Attribute.ForeignRecordName, fieldMap.Attribute.ForeignParentFieldName, fieldMap.Attribute.ForeignChildFieldName, recordAttribute.Name, parentId, proxyType, connection);
			if (proxyType == null)
			{
				foreach(IRecord record in linkedRecords)
				{
					RecordMap recordMap = broker.GetRecordMap(record.GetType());
					LookupChildRecords(record, recordMap, connection);
				}
			}
			return linkedRecords;

			/*
			}
			else
			{
				linkedRecords = new List<IRecord>();
				using (SQLiteDataReader reader = GetSQLiteDataReader(fieldMap.Attribute.ForeignRecordName, fieldMap.Attribute.ForeignParentFieldName, parentId, connection))
				{
					if (reader != null && reader.HasRows)
					{
						while(reader.Read())
						{
							string childId = reader[fieldMap.Attribute.ForeignChildFieldName].ToString();
							IRecord record = LookupParentRecord(recordAttribute.Name, "Id", childId, connection);
							RecordMap recordMap = broker.GetRecordMap(record.GetType());
							LookupChildRecords(record, recordMap, null, connection);
							linkedRecords.Add(record);
						}
					}
				}
			}
			*/
		}
		#endregion

		#region GetChildRecordList
		private IList<IRecord> GetChildRecordList(RecordAttribute recordAttribute, FieldMap fieldMap, string parentId, SQLiteConnection connection)
		{
			System.Diagnostics.Debug.WriteLine(string.Format("GetChildRecordList: {0}.{1}({2})", recordAttribute.Name, fieldMap.Property.Name, parentId));
		
			IList<IRecord> childRecords = null;
			
			Type proxyType = null;
			if ((FieldLoadOptions.Proxy & fieldMap.Attribute.LoadOptions) == FieldLoadOptions.Proxy)
				proxyType = fieldMap.Attribute.ChildType;
			
			if (fieldMap.Attribute.Relationship == FieldRelationship.OneToOne || fieldMap.Attribute.Relationship == FieldRelationship.OneToMany)
			{				
				childRecords = LookupRecords(recordAttribute.Name, fieldMap.Attribute.ForeignParentFieldName, parentId, proxyType, connection);
			}
			else if (fieldMap.Attribute.Relationship == FieldRelationship.ManyToOne || fieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
			{
				childRecords = LookupLinkedRecords(fieldMap, parentId, proxyType, connection);
			}

			return childRecords;
		}
		#endregion

		#region SaveParentRecord
		private void SaveParentRecord(IRecord record, RecordMap recordMap, DbTransaction transaction)
		{
			string saveFormat = "REPLACE INTO {0} ({1}) VALUES ({2})";

			SQLiteCommand saveCommand = new SQLiteCommand(string.Empty, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
						
			StringBuilder columns = new StringBuilder();
			StringBuilder values = new StringBuilder();

			for (int i = 1; i <= recordMap.BasicFieldMaps.Count; i++)
			{
				if (i > 1)
				{
					columns.Append(", ");
					values.Append(", ");
				}
				
				string columnName = GetSQLiteFieldName(recordMap.BasicFieldMaps[i]);
				columns.Append(columnName);
				
				object propertyValue = recordMap.BasicFieldMaps[i].Property.GetValue(record, null);
				string fieldValue = GetSQLiteFieldValue(record, recordMap.BasicFieldMaps[i], propertyValue);
				string valueParameterName = string.Format("@Value{0}", i);
				values.Append(valueParameterName);
				saveCommand.Parameters.Add(new SQLiteParameter(valueParameterName, fieldValue));
			}

			//Add the RecordTypeId
			columns.AppendFormat(", {0}", RECORD_TYPE_ID);
			
			string recordTypeIdValueParameterName = "@RecordTypeIdValue";
			values.AppendFormat(", {0}", recordTypeIdValueParameterName);
			saveCommand.Parameters.Add(new SQLiteParameter(recordTypeIdValueParameterName, recordMap.RecordTypeAttribute.Id));

			saveCommand.CommandText = string.Format(saveFormat, recordMap.RecordAttribute.Name, columns, values);
			saveCommand.ExecuteNonQuery();
		}
		#endregion

		#region SaveChildRecords
		private void SaveChildRecords(IRecord record, RecordMap recordMap, DbTransaction transaction)
		{
			foreach (FieldMap fieldMap in recordMap.AdvancedFieldMaps)
			{
				if ((fieldMap.Attribute.Cascades & FieldCascades.Save) == FieldCascades.Save)
				{
					//NOTE: Only save parent fields - child fields are simply a friendly reference back to the parent
					if (fieldMap.Attribute.Type == FieldType.Parent)
					{
						//TODO: find a way to cast to a generic IList<IRecord>			
						IList list = fieldMap.Property.GetValue(record, null) as IList;
						if (list != null)
						{
							foreach (IRecord childRecord in list)
							{
								if (childRecord != null)
									SaveRecord(childRecord, transaction);
							}
						}
						else
						{
							IRecord childRecord = (IRecord)fieldMap.Property.GetValue(record, null);
							if (childRecord != null)
								SaveRecord(childRecord, transaction);
						}
					}
				}
			}
		}
		#endregion
		
		#region SaveLinkedRecords
		private void SaveLinkedRecords(IRecord record, RecordMap recordMap, DbTransaction transaction)
		{
			foreach (LinkRecord linkRecord in recordMap.LinkRecords)
			{
				if ((linkRecord.FieldMap.Attribute.Cascades & FieldCascades.Save) == FieldCascades.Save)
				{
					if (linkRecord.FieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
					{
						if (linkRecord.FieldMap.Attribute.Type == FieldType.Parent)
						{
							string cleanupText = string.Format("DELETE FROM {0} WHERE {1} = @IdValue", linkRecord.Name, linkRecord.ParentFieldName);
							SQLiteCommand cleanupCommand = new SQLiteCommand(cleanupText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
							cleanupCommand.Parameters.Add(new SQLiteParameter("@IdValue", record.Id));
							cleanupCommand.ExecuteNonQuery();

							IList linkList = (IList)linkRecord.FieldMap.Property.GetValue(record, null);
							foreach (IRecord childRecord in linkList)
							{
								//WARNING: There is a bug (probably in System.Data.SQLite) with using parameters to set fields
								//that make up an aggregate UNIQUE constraint - the data is managled.  The work around is to pass
								//the values in directly without using parameters which is used below.
								
								//string linkText = string.Format("INSERT INTO {0} ({1}, {2}) VALUES (@ParentIdValue, @ChildIdValue)", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName);
								string linkText = string.Format("REPLACE INTO {0} ({1}, {2}) VALUES ('{3}', '{4}')", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName, record.Id, childRecord.Id);
								SQLiteCommand linkCommand = new SQLiteCommand(linkText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
								//linkCommand.Parameters.Add(new SQLiteParameter("@ParentIdValue", record.Id));
								//linkCommand.Parameters.Add(new SQLiteParameter("@ChildIdValue", childRecord.Id));
								linkCommand.ExecuteNonQuery();
							}
						}
					}
				}
			}
		}
		#endregion

		#region DeleteParentRecord
		private void DeleteParentRecord(IRecord record, RecordMap recordMap, DbTransaction transaction)
		{
			string deleteFormat = "DELETE FROM {0} WHERE Id = '{1}'";
			string commandText = string.Format(deleteFormat, recordMap.RecordAttribute.Name, record.Id);

			SQLiteCommand deleteCommand = new SQLiteCommand(commandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
			deleteCommand.ExecuteNonQuery();
		}
		#endregion

		#region DeleteChildRecords
		private void DeleteChildRecords(IRecord record, RecordMap recordMap, DbTransaction transaction)
		{
			foreach (FieldMap fieldMap in recordMap.AdvancedFieldMaps)
			{
				if ((fieldMap.Attribute.Cascades & FieldCascades.Delete) == FieldCascades.Delete)
				{
					//TODO: find a way to cast to a generic IList<IRecord>			
					IList list = fieldMap.Property.GetValue(record, null) as IList;
					if (list != null)
					{
						foreach (IRecord childRecord in list)
						{
							DeleteRecord(childRecord, transaction);
						}
					}
					else
					{
						IRecord childRecord = (IRecord)fieldMap.Property.GetValue(record, null);
						DeleteRecord(childRecord, transaction);
					}
				}
			}
		}
		#endregion
		
		#region Delete LinkedRecord
		private void DeleteLinkedRecords(IRecord record, RecordMap recordMap, DbTransaction transaction)
		{
			foreach (LinkRecord linkRecord in recordMap.LinkRecords)
			{
				if ((linkRecord.FieldMap.Attribute.Cascades & FieldCascades.Save) == FieldCascades.Save)
				{
					if (linkRecord.FieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
					{
						if (linkRecord.FieldMap.Attribute.Type == FieldType.Parent)
						{
							string cleanupText = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", linkRecord.Name, linkRecord.ParentFieldName, record.Id);
							SQLiteCommand cleanupCommand = new SQLiteCommand(cleanupText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
							cleanupCommand.ExecuteNonQuery();
						}
					}
				}
			}
		}
		#endregion

		#endregion
		
		//TODO: move these into a static utility class
		#region Internal Static Methods

		#region GetSQLiteFieldRequired
		internal static string GetSQLiteFieldRequired(bool isRequired)
		{
			return (isRequired) ? "NOT NULL" : string.Empty;
		}
		#endregion

		#region GetSQLiteFieldDefault
		internal static string GetSQLiteFieldDefault(TypeAffinity type, object defaultValue)
		{
			switch(type)
			{
				case TypeAffinity.Text:
					if (defaultValue == null) defaultValue = string.Empty;
					return string.Format("DEFAULT '{0}'", defaultValue);
				case TypeAffinity.Double:
				case TypeAffinity.Int64:
					if (defaultValue == null) defaultValue = 0;
					return string.Format("DEFAULT {0}", defaultValue);
				default:
					return string.Empty;
			}
		}
		#endregion
		
		#region GetSQLiteFieldType
		internal static string GetSQLiteFieldType(TypeAffinity type)
		{
			string dbType = "TEXT";

			if (type == TypeAffinity.Uninitialized)
				dbType = string.Empty;

			if (type == TypeAffinity.Int64)
				dbType = "INTEGER";

			if (type == TypeAffinity.Double)
				dbType = "REAL";

			return dbType;
		}
		#endregion
		
		#endregion

		#region IPersistenceMechanism Members
		public string Name
		{
			get { return "SQLite Embedded Database"; }
		}

		public IPersistenceBroker Broker
		{
			get { return broker; }
			set { broker = value; }
		}

		public DbConnection GetConnection()
		{
			return GetSQLiteConnection();
		}

		public void InitializeRecordMap(RecordMap recordMap, DbTransaction transaction)
		{			
			InitializeParentRecordMap(recordMap, (SQLiteTransaction)transaction);
			InitializeLinkedRecordMaps(recordMap, (SQLiteTransaction)transaction);
		}

		public T LookupRecord<T>(Guid id, DbConnection connection) where T: IRecord
		{
			if (connection != null)
			{				
				RecordAttribute recordAttribute = broker.RecordAttributes[typeof(T)];
				if (recordAttribute != null)
				{					
					IRecord record = LookupParentRecord(recordAttribute.Name, "Id", id.ToString(), recordAttribute.ProxyType, (SQLiteConnection)connection);
					if (record != null)
					{
						if (!record.IsProxy)
						{
							RecordMap recordMap = broker.GetRecordMap(record.GetType());
							LookupChildRecords(record, recordMap, (SQLiteConnection)connection);
						}
						return (T)record;
					}
					else return default(T);
				}
				else throw new ApplicationException("SQLite could not lookup the record attribute for this type");
			}
			else throw new ArgumentNullException("connection");
		}

		public void SaveRecord(IRecord record, DbTransaction transaction)
		{
			if (record != null)
			{
				if (transaction != null)
				{
					RecordMap recordMap = broker.GetRecordMap(record.GetType());				
					if (recordMap != null)
					{
						SaveParentRecord(record, recordMap, transaction);
						SaveChildRecords(record, recordMap, transaction);
						SaveLinkedRecords(record, recordMap, transaction);
					}
					else throw new ApplicationException("SQLite could not lookup the record map for this type");
				}
				else throw new ArgumentNullException("transaction");
			}
			else throw new ArgumentNullException("record");
		}
		
		public void DeleteRecord(IRecord record, DbTransaction transaction)
		{
			if (record != null)
			{				
				if (transaction != null)
				{
					RecordMap recordMap = broker.GetRecordMap(record.GetType());
					if (recordMap != null)
					{
						DeleteParentRecord(record, recordMap, transaction);
						DeleteChildRecords(record, recordMap, transaction);
						DeleteLinkedRecords(record, recordMap, transaction);
					}
					else throw new ApplicationException("SQLite could not lookup the record map for this type");
				}
				else throw new ArgumentNullException("transaction");
			}
			else throw new ArgumentNullException("record");
		}
		#endregion
	}
}
