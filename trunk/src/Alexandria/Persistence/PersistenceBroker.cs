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
			RecordMap map = new RecordMap(mechanism, table.Rows[0]);
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
		#endregion
	}
}
