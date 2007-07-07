using System;
using System.Collections.Generic;
using System.Data;
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
			this.mechanism = mechanism;
			
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
					
						RecordMap recordMap = GetRecordMap(type);
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

		private RecordTypeAttribute GetRecordTypeAttribute(Type type)
		{
			RecordTypeAttribute recordTypeAttribute = null;
			foreach (Attribute attribute in type.GetCustomAttributes(typeof(RecordTypeAttribute), false))
			{
				recordTypeAttribute = (RecordTypeAttribute)attribute;
				break;
			}
			return recordTypeAttribute;
		}

		private RecordMap GetRecordMap(Type type)
		{
			RecordMap recordMap = null;
			RecordTypeAttribute recordTypeAttribute = GetRecordTypeAttribute(type);
			if (recordTypeAttribute != null)
			{
				IDictionary<int, PropertyMap> basicProperties = new Dictionary<int, PropertyMap>();
				IList<PropertyMap> advancedProperties = new List<PropertyMap>();

				foreach (PropertyInfo property in type.GetProperties())
				{
					foreach (Attribute attribute in property.GetCustomAttributes(typeof(PropertyAttribute), true))
					{
						PropertyAttribute propertyAttribute = (PropertyAttribute)attribute;
						if (propertyAttribute.Ordinal > 0)
							basicProperties.Add(propertyAttribute.Ordinal, new PropertyMap(propertyAttribute, property));
						else advancedProperties.Add(new PropertyMap(propertyAttribute, property));
					}
				}
				foreach (Type interfaceType in type.GetInterfaces())
				{
					foreach (PropertyInfo interfaceProperty in interfaceType.GetProperties())
					{
						foreach (Attribute attribute in interfaceProperty.GetCustomAttributes(typeof(PropertyAttribute), true))
						{
							PropertyAttribute propertyAttribute = (PropertyAttribute)attribute;
							if (propertyAttribute.Ordinal > 0)
								basicProperties.Add(propertyAttribute.Ordinal, new PropertyMap(propertyAttribute, interfaceProperty));
							else advancedProperties.Add(new PropertyMap(propertyAttribute, interfaceProperty));
						}
					}
				}

				recordMap = new RecordMap(type, recordTypeAttribute, basicProperties, advancedProperties);
			}

			return recordMap;
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
				
				factory = factoryConstructor.Invoke(new object[0]);
				
				foreach(MethodInfo method in type.GetMethods(BindingFlags.Static|BindingFlags.Public))
				{
					foreach (FactoryAttribute attribute in method.GetCustomAttributes(typeof(FactoryAttribute), false))
					{
						FactoryAttribute factoryAttribute = (FactoryAttribute)attribute;
						return new FactoryMap(factoryAttribute, factory, method);
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
		}

		public IDictionary<Type, RecordAttribute> RecordAttributes
		{
			get { return recordAttributes; }
		}
		
		public IDictionary<string, FactoryMap> FactoryMaps
		{
			get { return factoryMaps; }
		}
				
		public IDictionary<string, RecordMap> RecordMaps
		{
			get { return recordMaps; }
		}
				
		public T LookupRecord<T>(Guid id) where T : IRecord
		{			
			DataTable table = mechanism.GetDataTable(this, typeof(T));			
			return default(T);
		}

		public void SaveRecord(IRecord record)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void DeleteRecord(IRecord record)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void ConnectTo(IPersistenceMechanism mechanism)
		{
			this.mechanism = mechanism;
		}
		#endregion
	}
}
