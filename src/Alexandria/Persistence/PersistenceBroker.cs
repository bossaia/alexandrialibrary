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
		private IDictionary<string, ConstructorMap> constructors = new Dictionary<string, ConstructorMap>();
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
						if (type.IsClass || type.IsValueType)
						{
							foreach (ConstructorInfo constructor in type.GetConstructors())
							{
								foreach (ConstructorAttribute ctorAttribute in constructor.GetCustomAttributes(typeof(ConstructorAttribute), false))
								{								
									ConstructorAttribute constructorAttribute = (ConstructorAttribute)ctorAttribute;
									ConstructorMap map = new ConstructorMap(constructorAttribute, constructor);
									constructors.Add(constructorAttribute.RecordTypeId, map);
								}
							}						
						}
						else if (type.IsInterface)
						{
							RecordAttribute recordAttribute = null;							
							foreach (Attribute typeAttribute in type.GetCustomAttributes(typeof(RecordAttribute), false))
							{
								recordAttribute = (RecordAttribute)typeAttribute;
							}
							
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
								
								RecordProperties properties = new RecordProperties(type, recordAttribute, basicProperties, advancedProperties);
								recordProperties.Add(type, properties);
							}
						}
					}					
				}
			}
		}
		#endregion
	
		#region IPersistenceBroker Members
		public IPersistenceMechanism Mechanism
		{
			get { return mechanism; }
		}
		
		public IDictionary<Type, RecordProperties> RecordProperties
		{
			get { return recordProperties; }
		}
		
		public IDictionary<string, ConstructorMap> Constructors
		{
			get { return constructors; }
		}
		
		public T LookupRecord<T>(Guid id) where T : IRecord
		{
			//RecordAttribute recordAttribute = RecordAttributes[typeof(T)];
			RecordProperties properties = RecordProperties[typeof(T)];
			DataTable table = mechanism.GetDataTable(properties.RecordAttribute.Name, properties.RecordAttribute.IdField, id.ToString());
			//ConstructorMap constructorMap = Constructors[table.Rows[0][RECORD_TYPE_ID].ToString()];
			//RecordMap map = new RecordMap(mechanism, table.Rows[0], constructorMap);
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
			throw new Exception("The method or operation is not implemented.");
		}

		public void DisconnectFrom(IPersistenceMechanism mechanism)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
