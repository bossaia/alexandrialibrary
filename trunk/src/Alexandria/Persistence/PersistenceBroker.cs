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
using System.Reflection;
using Alexandria.Plugins;

namespace Alexandria.Persistence
{
	public class PersistenceBroker : IPersistenceBroker
	{
		#region Constructors
		public PersistenceBroker(IPluginRepository repository, IPersistenceMechanism mechanism)
		{
			this.repository = repository;
			Mechanism = mechanism;
			
			Initialize();
			int x = recordMaps.Count;
		}
		#endregion
	
		#region Private Constants
		private const string RECORD_TYPE_ID = "RecordTypeId";
		#endregion
	
		#region Private Fields
		private IPluginRepository repository;
		private IPersistenceMechanism mechanism;
		private IDictionary<Type, RecordAttribute> recordAttributes = new Dictionary<Type, RecordAttribute>();
		private IDictionary<string, FactoryMap> factoryMaps = new Dictionary<string, FactoryMap>();
		private IDictionary<string, RecordMap> recordMaps = new Dictionary<string, RecordMap>();
		#endregion
		
		#region Private Methods
		private void Initialize()
		{
			foreach(Assembly assembly in repository.Assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					FactoryMap factoryMap = GetFactoryMap(type);
					if (factoryMap != null)
					{
						factoryMaps.Add(factoryMap.Attribute.RecordTypeId, factoryMap);
					}
				
					if (type.GetInterface("IRecord") != null)
					{
						RecordAttribute recordAttribute = GetRecordAttribute(type);
						if (recordAttribute != null)
						{
							recordAttributes.Add(type, recordAttribute);
						}
					
						RecordMap recordMap = GetRecordMap(type, recordAttribute);
						if (recordMap != null)
						{
							recordMaps.Add(recordMap.RecordTypeAttribute.Id, recordMap);
						}
					}				
				}
			}			
		}

		private RecordAttribute GetRecordAttribute(Type type)
		{
			RecordAttribute recordAttribute = null;
			foreach (Attribute typeAttribute in type.GetCustomAttributes(typeof(RecordAttribute), false))
			{
				recordAttribute = (RecordAttribute)typeAttribute;
				break;
			}
			return recordAttribute;
		}

		private RecordMap GetRecordMap(Type type, RecordAttribute recordAttribute)
		{
			RecordMap recordMap = null;
			RecordTypeAttribute recordTypeAttribute = GetRecordTypeAttribute(type);
			if (recordTypeAttribute != null)
			{
				IDictionary<int, FieldMap> basicFieldMaps = new Dictionary<int, FieldMap>();
				IList<FieldMap> advancedFieldMaps = new List<FieldMap>();
				IList<LinkRecord> linkRecords = new List<LinkRecord>();

				foreach (PropertyInfo property in type.GetProperties())
				{
					foreach (Attribute attribute in property.GetCustomAttributes(typeof(FieldAttribute), true))
					{
						FieldAttribute fieldAttribute = (FieldAttribute)attribute;
						FieldMap fieldMap = new FieldMap(fieldAttribute, property);
						
						if (fieldAttribute.Location == FieldLocation.Local)
							basicFieldMaps.Add(fieldAttribute.Ordinal, fieldMap);
						else if (fieldAttribute.Location == FieldLocation.Foreign)
						{
							advancedFieldMaps.Add(fieldMap);

							if (!string.IsNullOrEmpty(fieldAttribute.ForeignParentFieldName))
							{
								linkRecords.Add(new LinkRecord(fieldAttribute.ForeignRecordName, fieldAttribute.ForeignParentFieldName, fieldAttribute.ForeignChildFieldName, fieldMap));
							}
						}
					}
				}
				foreach (Type interfaceType in type.GetInterfaces())
				{
					foreach (PropertyInfo interfaceProperty in interfaceType.GetProperties())
					{
						foreach (Attribute attribute in interfaceProperty.GetCustomAttributes(typeof(FieldAttribute), true))
						{
							FieldAttribute fieldAttribute = (FieldAttribute)attribute;
							FieldMap fieldMap = new FieldMap(fieldAttribute, interfaceProperty);
							
							if (fieldAttribute.Location == FieldLocation.Local)
								basicFieldMaps.Add(fieldAttribute.Ordinal, fieldMap);
							else if (fieldAttribute.Location == FieldLocation.Foreign)
							{
								advancedFieldMaps.Add(fieldMap);

								if (!string.IsNullOrEmpty(fieldAttribute.ForeignParentFieldName))
								{
									linkRecords.Add(new LinkRecord(fieldAttribute.ForeignRecordName, fieldAttribute.ForeignParentFieldName, fieldAttribute.ForeignChildFieldName, fieldMap));
								}
							}
						}
					}
				}

				recordMap = new RecordMap(type, recordAttribute, recordTypeAttribute, basicFieldMaps, advancedFieldMaps, linkRecords);
			}

			return recordMap;
		}

		public RecordTypeAttribute GetRecordTypeAttribute(Type type)
		{
			RecordTypeAttribute recordTypeAttribute = null;
			foreach (Attribute attribute in type.GetCustomAttributes(typeof(RecordTypeAttribute), false))
			{
				recordTypeAttribute = (RecordTypeAttribute)attribute;
				break;
			}
			return recordTypeAttribute;
		}
		
		private FactoryMap GetFactoryMap(Type type)
		{
			if (type.IsClass || type.IsValueType)
			{
				ConstructorInfo factoryConstructor = null;
				object factory = null;
			
				foreach (ConstructorInfo constructor in type.GetConstructors(BindingFlags.Public|BindingFlags.Instance))
				{
					if (constructor.GetParameters().Length == 0)
					{
						factoryConstructor = constructor;
					}
					
					foreach (FactoryAttribute attribute in constructor.GetCustomAttributes(typeof(FactoryAttribute), false))
					{
						FactoryAttribute factoryAttribute = (FactoryAttribute)attribute;
						return new FactoryMap(factoryAttribute, constructor);
					}
				}
				
				foreach(MethodInfo method in type.GetMethods(BindingFlags.Static|BindingFlags.Public))
				{
					foreach (FactoryAttribute attribute in method.GetCustomAttributes(typeof(FactoryAttribute), false))
					{
						if (factoryConstructor != null)
						{
							factory = factoryConstructor.Invoke(new object[0]);
							FactoryAttribute factoryAttribute = (FactoryAttribute)attribute;
							return new FactoryMap(factoryAttribute, factory, method);
						}
						else throw new ApplicationException("Could not find the factory for type: " + type.Name);
					}
				}
			}
			return null;
		}
		#endregion
	
		#region IPersistenceBroker Members
		public IPersistenceMechanism Mechanism
		{
			get { return mechanism; }
			set
			{
				mechanism = value;
				mechanism.Broker = this;
			}
		}
		
		public IDictionary<string, FactoryMap> FactoryMaps
		{
			get { return factoryMaps; }
		}
				
		public IDictionary<string, RecordMap> RecordMaps
		{
			get { return recordMaps; }
		}

		public RecordMap GetRecordMap(Type type)
		{
			RecordTypeAttribute attribute = GetRecordTypeAttribute(type);
			if (attribute != null)
			{
				return RecordMaps[attribute.Id];
			}
			else return null;
		}

		public void InitializeRecordMaps()
		{
			using (DbConnection connection = mechanism.GetConnection())
			{
				connection.Open();
				DbTransaction transaction = null;
				try
				{
					transaction = connection.BeginTransaction();
					foreach (RecordMap recordMap in recordMaps.Values)
					{
						mechanism.InitializeRecordMap(recordMap, transaction);
					}
					transaction.Commit();
				}
				catch (Exception ex)
				{
					if (transaction != null)
						transaction.Rollback();

					throw ex;
				}
			}
		}
		
		public T LookupRecord<T>(Guid id) where T : IRecord
		{
			using (DbConnection connection = mechanism.GetConnection())
			{
				connection.Open();
				T record = mechanism.LookupRecord<T>(id, connection);
				record.PersistenceBroker = this;
				return record;
			}
		}

		public void SaveRecord(IRecord record)
		{			
			using (DbConnection connection = mechanism.GetConnection())
			{
				connection.Open();
				DbTransaction transaction = null;
				try
				{
					transaction = connection.BeginTransaction();
					mechanism.SaveRecord(record, transaction);
					//transaction.Rollback();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					if (transaction != null)
						transaction.Rollback();
						
					throw ex;
				}
			}
		}

		public void DeleteRecord(IRecord record)
		{
			using (DbConnection connection = mechanism.GetConnection())
			{
				connection.Open();
				DbTransaction transaction = null;
				try
				{
					transaction = connection.BeginTransaction();
					mechanism.DeleteRecord(record, transaction);
					transaction.Commit();
				}
				catch (Exception ex)
				{
					if (transaction != null)
						transaction.Rollback();

					throw ex;
				}
			}
		}
		#endregion
	}
}
