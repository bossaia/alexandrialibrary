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
		private IDictionary<string, RecordProperties> recordProperties = new Dictionary<string, RecordProperties>();
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
						ConstructorMap constructorMap = new ConstructorMap();
						if (GetConstructorMap(type, ref constructorMap))
						{
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
								
								RecordProperties properties = new RecordProperties(type, constructorMap, recordAttribute, basicProperties, advancedProperties);
								recordProperties.Add(constructorMap.Attribute.RecordTypeId, properties);
							}
						}
					}					
				}
			}
		}
		
		private bool GetConstructorMap(Type type, ref ConstructorMap constructorMap)
		{
			if (type.IsClass || type.IsValueType)
			{
				foreach (ConstructorInfo constructor in type.GetConstructors())
				{
					foreach (ConstructorAttribute ctorAttribute in constructor.GetCustomAttributes(typeof(ConstructorAttribute), false))
					{
						ConstructorAttribute constructorAttribute = (ConstructorAttribute)ctorAttribute;
						constructorMap = new ConstructorMap(constructorAttribute, constructor);
						return true;
					}
				}
			}
			return false;
		}
		#endregion
	
		#region IPersistenceBroker Members
		public IPersistenceMechanism Mechanism
		{
			get { return mechanism; }
		}
		
		public IDictionary<string, RecordProperties> RecordProperties
		{
			get { return recordProperties; }
		}
		
		public RecordAttribute GetRecordAttribute(Type type)
		{
			RecordAttribute recordAttribute = null;
			foreach (Attribute typeAttribute in type.GetCustomAttributes(typeof(RecordAttribute), false))
			{
				recordAttribute = (RecordAttribute)typeAttribute;
				break;
			}
			return recordAttribute;
		}
		
		public T LookupRecord<T>(Guid id) where T : IRecord
		{
			RecordAttribute recordAttribute = GetRecordAttribute(typeof(T));
			DataTable table = mechanism.GetDataTable(recordAttribute.Name, recordAttribute.IdField, id.ToString());
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
