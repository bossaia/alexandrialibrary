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
				else return Convert.ToDateTime(value.ToString());
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
		private SQLiteDataReader GetSQLiteDataReader(string recordName, string fieldName, string value, SQLiteConnection connection)
		{
			string commandText = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", recordName, fieldName, value);
			SQLiteCommand selectCommand = new SQLiteCommand(commandText, connection);
			return selectCommand.ExecuteReader();
		}
		#endregion

		#region FieldIsLink
		private bool FieldIsLink(RecordMap recordMap, FieldMap fieldMap)
		{
			foreach(LinkRecord linkRecord in recordMap.LinkRecords)
			{
				if (linkRecord.FieldMap.Property == fieldMap.Property)
					return true;
			}
			
			return false;
		}
		#endregion

		#region InitializeParentRecordMap
		private void InitializeParentRecordMap(RecordMap recordMap, DbTransaction transaction)
		{
			string createFormat = "CREATE TABLE IF NOT EXISTS {0} ({1})";
			StringBuilder columns = new StringBuilder();
			for (int i = 1; i <= recordMap.BasicFieldMaps.Count; i++)
			{
				if (i > 1) columns.Append(", ");

				string fieldName = GetSQLiteFieldName(recordMap.BasicFieldMaps[i]);
				string fieldType = GetSQLiteFieldType(recordMap.BasicFieldMaps[i].Property.PropertyType);
				string fieldConstraints = GetSQLiteFieldConstraints(recordMap.BasicFieldMaps[i].Attribute.Constraints);

				columns.AppendFormat("{0} {1}{2}", fieldName, fieldType, fieldConstraints);
			}

			// Add the RecordTypeId
			columns.AppendFormat(", {0} TEXT NOT NULL", RECORD_TYPE_ID);

			string commandText = string.Format(createFormat, recordMap.RecordAttribute.Name, columns);
			SQLiteCommand createTable = new SQLiteCommand(commandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
			createTable.ExecuteNonQuery();
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
						string linkCommandText = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1} NOT NULL, {2} NOT NULL, UNIQUE ({1}, {2}))", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName);
						SQLiteCommand createLinkTable = new SQLiteCommand(linkCommandText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
						createLinkTable.ExecuteNonQuery();
					}
				}
			}
		}
		#endregion

		#region LookupRecords
		private IList<IRecord> LookupRecords(string recordName, string fieldName, string value, SQLiteConnection connection)
		{
			IList<IRecord> records = new List<IRecord>();
			
			using (SQLiteDataReader reader = GetSQLiteDataReader(recordName, fieldName, value, connection))
			{
				if (reader != null && reader.HasRows)
				{
					while(reader.Read())
					{
						string recordTypeId = null;
						if (reader[RECORD_TYPE_ID] != null)
							recordTypeId = reader[RECORD_TYPE_ID].ToString();
							
						if (!string.IsNullOrEmpty(recordTypeId))
						{
							FactoryMap factoryMap = broker.FactoryMaps[recordTypeId];
							RecordMap recordMap = broker.RecordMaps[recordTypeId];
							if (factoryMap != null)
							{
								if (recordMap != null)
								{
									ParameterInfo[] parameterInfo = factoryMap.GetParameters();
									object[] parameters = new object[parameterInfo.Length];
									for (int i = 0; i < parameterInfo.Length; i++)
									{
										string paramFieldName = GetFactoryFieldName(parameterInfo[i].Name); //, recordMap.BasicFieldMaps[i+1]);
										object paramValue = GetRecordValueFromDatabaseValue(parameterInfo[i].ParameterType, reader[paramFieldName]);
										parameters[i] = paramValue;
									}

									IRecord record = factoryMap.GetRecord(parameters);
									record.PersistenceBroker = broker;
									records.Add(record);
								}
								else throw new ApplicationException("SQLite could not determine the record map for this record");
							}
							else throw new ApplicationException("SQLite could not determine the factory map for this record");
						}
						else throw new ApplicationException("SQLite could not determine the type of this record");
					}
				}
			}
			
			return records;
		}
		#endregion

		#region LookupParentRecord
		private IRecord LookupParentRecord(string recordName, string fieldName, string value, SQLiteConnection connection)
		{
			IList<IRecord> records = LookupRecords(recordName, fieldName, value, connection);
			return records[0];
		}
		#endregion
		
		#region LookupChildRecords
		private void LookupChildRecords(IRecord record, RecordMap recordMap, SQLiteConnection connection)
		{
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
						string recordName = recordAttribute.Name;
						string fieldName = fieldMap.Attribute.ForeignParentFieldName;
					
						if (fieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
						{
							recordName = fieldMap.Attribute.ForeignRecordName;
						}
					
						IList<IRecord> childRecordList = LookupRecords(recordName, fieldName, record.Id.ToString(), connection);
						if (fieldMap.Attribute.Relationship == FieldRelationship.OneToOne)
						{
							if (childRecordList != null && childRecordList.Count > 0)
							{
								IRecord childRecord = childRecordList[0];
								childRecord.Parent = record;
								fieldMap.Property.SetValue(record, childRecord, null);
							}
						}
						else if (fieldMap.Attribute.Relationship == FieldRelationship.OneToMany || fieldMap.Attribute.Relationship == FieldRelationship.ManyToMany)
						{
							if (propertyList != null && childRecordList != null)
							{
								foreach(IRecord childRecord in childRecordList)
								{
									childRecord.Parent = record;
									propertyList.Add(childRecord);
								}
							}
						}
					}
					else throw new ApplicationException("SQLite could not lookup record attribute for child record");
				}
				else if (fieldMap.Attribute.Type == FieldType.Child)
				{
					string y = fieldMap.Property.Name;
				}
			}
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
								string linkText = string.Format("INSERT INTO {0} ({1}, {2}) VALUES (@ParentIdValue, @ChildIdValue)", linkRecord.Name, linkRecord.ParentFieldName, linkRecord.ChildFieldName);
								SQLiteCommand linkCommand = new SQLiteCommand(linkText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
								linkCommand.Parameters.Add(new SQLiteParameter("@ParentIdValue", record.Id));
								linkCommand.Parameters.Add(new SQLiteParameter("@ChildIdValue", childRecord.Id));
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
			InitializeParentRecordMap(recordMap, transaction);
			InitializeLinkedRecordMaps(recordMap, transaction);
		}

		public T LookupRecord<T>(Guid id, DbConnection connection) where T: IRecord
		{
			if (connection != null)
			{				
				RecordAttribute recordAttribute = broker.RecordAttributes[typeof(T)];
				if (recordAttribute != null)
				{					
					IRecord record = LookupParentRecord(recordAttribute.Name, "Id", id.ToString(), (SQLiteConnection)connection);
					RecordMap recordMap = broker.GetRecordMap(record.GetType());
					LookupChildRecords(record, recordMap, (SQLiteConnection)connection);
					return (T)record;
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
