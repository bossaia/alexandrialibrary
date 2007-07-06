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
			int x = recordProperties.Count;
		}
		#endregion
	
		#region Private Constants
		private const string RECORD_TYPE_ID = "RecordTypeId";
		#endregion
	
		#region Private Fields
		private IPluginRepository repository;
		private IPersistenceMechanism mechanism;		
		private IDictionary<string, ConstructorMap> constructorMaps = new Dictionary<string, ConstructorMap>();
		private IDictionary<Type, RecordProperties> recordProperties = new Dictionary<Type, RecordProperties>();
		#endregion
		
		#region Private Methods
		private void Initialize()
		{
			foreach(Assembly assembly in repository.Assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					if (type.GetInterface("IRecord") != null)
					{
						RecordProperties properties = GetRecordProperties(type);
						if (properties != null)
						{	
							recordProperties.Add(type, properties);
										
							ConstructorMap constructorMap = GetConstructorMap(type);
							if (constructorMap != null)
							{
								constructorMaps.Add(constructorMap.Attribute.RecordTypeId, constructorMap);
							}
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

		private RecordProperties GetRecordProperties(Type type)
		{
			RecordProperties recordProperties = null;
			RecordAttribute recordAttribute = GetRecordAttribute(type);
			if (recordAttribute != null)
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

				recordProperties = new RecordProperties(type, recordAttribute, basicProperties, advancedProperties);
			}

			return recordProperties;
		}
		
		private ConstructorMap GetConstructorMap(Type type)
		{
			if (type.IsClass || type.IsValueType)
			{
				foreach (ConstructorInfo constructor in type.GetConstructors())
				{
					foreach (ConstructorAttribute ctorAttribute in constructor.GetCustomAttributes(typeof(ConstructorAttribute), false))
					{
						ConstructorAttribute constructorAttribute = (ConstructorAttribute)ctorAttribute;
						return new ConstructorMap(constructorAttribute, constructor);
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
		
		public IDictionary<string, ConstructorMap> ConstructorMaps
		{
			get { return constructorMaps; }
		}
		
		public IDictionary<Type, RecordProperties> RecordProperties
		{
			get { return recordProperties; }
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
