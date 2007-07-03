using System;
using System.Collections.Generic;
using System.Reflection;

namespace Alexandria.Persistence
{
	public interface IPersistenceBroker
	{
		IDictionary<string, ConstructorMap> ConstructorsByRecordType { get; }
		IDictionary<Type, ConstructorMap> ConstructorsByType { get; }
		T LookupRecord<T>(Guid id) where T: IRecord;
		void SaveRecord(IRecord record);
		void DeleteRecord(IRecord record);
		void ConnectTo(IPersistenceMechanism mechanism);
		void DisconnectFrom(IPersistenceMechanism mechanism);
	}
}
