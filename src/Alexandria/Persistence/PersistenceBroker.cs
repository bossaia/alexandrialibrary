using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
		}
		#endregion
	
		#region Private Fields
		private IPluginRepository repository;
		private IPersistenceMechanism mechanism;
		private IDictionary<string, ConstructorMap> constructorsByRecordType = new Dictionary<string, ConstructorMap>();
		private IDictionary<Type, ConstructorMap> constructorsByType = new Dictionary<Type, ConstructorMap>();
		#endregion
		
		#region Private Methods
		private void Initialize()
		{
			foreach(Assembly assembly in repository.Assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					Type persistent = type.GetInterface("IPersistent");
					if (persistent != null)
					{
						foreach (ConstructorInfo constructor in type.GetConstructors())
						{
							foreach (Attribute attribute in constructor.GetCustomAttributes(typeof(ConstructorAttribute), false))
							{
								//TODO: fix the logic for creating a constructor map
								ConstructorAttribute constructorAttribute = (ConstructorAttribute)attribute;
								ConstructorMap map = new ConstructorMap(null, constructorAttribute, constructor);
								constructorsByRecordType.Add(constructorAttribute.RecordTypeId, map);
								constructorsByType.Add(type, map);
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
		
		public IDictionary<string, ConstructorMap> ConstructorsByRecordType
		{
			get { return constructorsByRecordType; }
		}

		public IDictionary<Type, ConstructorMap> ConstructorsByType
		{
			get { return constructorsByType; }
		}
		
		public T LookupRecord<T>(Guid id) where T : IRecord
		{
			ConstructorMap constructorMap = ConstructorsByType[typeof(T)];
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
