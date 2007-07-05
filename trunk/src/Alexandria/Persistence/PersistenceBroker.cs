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
			int x = constructors.Count;
		}
		#endregion
	
		#region Private Constants
		private const string RECORD_TYPE_ID = "RecordTypeId";
		#endregion
	
		#region Private Fields
		private IPluginRepository repository;
		private IPersistenceMechanism mechanism;
		private IDictionary<string, ConstructorMap> constructors = new Dictionary<string, ConstructorMap>();
		private IDictionary<Type, RecordAttribute> recordAttributes = new Dictionary<Type, RecordAttribute>();
		#endregion
		
		#region Private Methods
		private void Initialize()
		{
			foreach(Assembly assembly in repository.Assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					foreach(Attribute typeAttribute in type.GetCustomAttributes(typeof(RecordAttribute), false))
					{
						RecordAttribute recordAttribute = (RecordAttribute)typeAttribute;
						recordAttributes.Add(type, recordAttribute);
					}
				
					Type persistent = type.GetInterface("IPersistent");
					if (persistent != null)
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
				}
			}
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
		
		public IDictionary<string, ConstructorMap> Constructors
		{
			get { return constructors; }
		}
		
		public T LookupRecord<T>(Guid id) where T : IRecord
		{
			RecordAttribute recordAttribute = RecordAttributes[typeof(T)];
			DataTable table = mechanism.GetDataTable(recordAttribute.Name, recordAttribute.IdField, id.ToString());
			ConstructorMap constructorMap = Constructors[table.Rows[0][RECORD_TYPE_ID].ToString()];
			RecordMap map = new RecordMap(mechanism, table.Rows[0], constructorMap);
			//ConstructorMap constructorMap = ConstructorsByType[typeof(T)];
			//RecordMap recordMap = new RecordMap(mechanism, 
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
		
		public void Test()
		{
			Dictionary<int, PropertyAttribute> orderedProperties = new Dictionary<int,PropertyAttribute>();
			List<PropertyAttribute> unorderedProperties = new List<PropertyAttribute>();
			Type type = typeof(Alexandria.Metadata.IAudioTrack);
			foreach(PropertyInfo property in type.GetProperties())
			{
				foreach(Attribute attribute in property.GetCustomAttributes(typeof(PropertyAttribute), true))
				{
					PropertyAttribute propertyAttribute = (PropertyAttribute)attribute;
					if (propertyAttribute.Ordinal > 0)
						orderedProperties.Add(propertyAttribute.Ordinal, propertyAttribute);
					else unorderedProperties.Add(propertyAttribute);
				}
			}
			foreach(Type interfaceType in type.GetInterfaces())
			{
				foreach(PropertyInfo interfaceProperty in interfaceType.GetProperties())
				{
					foreach (Attribute attribute in interfaceProperty.GetCustomAttributes(typeof(PropertyAttribute), true))
					{
						PropertyAttribute propertyAttribute = (PropertyAttribute)attribute;
						if (propertyAttribute.Ordinal > 0)
							orderedProperties.Add(propertyAttribute.Ordinal, propertyAttribute);
						else unorderedProperties.Add(propertyAttribute);
					}
				}
			}
			
			int x = orderedProperties.Count;
			int y = unorderedProperties.Count;
		}
		#endregion
	}
}
