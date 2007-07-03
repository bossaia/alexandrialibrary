using System;
using System.Collections.Generic;
using System.Reflection;

namespace Alexandria.Persistence
{
	public interface IPersistenceBroker
	{
		IDictionary<Type, RecordAttribute> RecordAttributes { get; }
		IDictionary<string, ConstructorMap> Constructors { get; }
		T LookupRecord<T>(Guid id) where T: IRecord;
		void SaveRecord(IRecord record);
		void DeleteRecord(IRecord record);
		void ConnectTo(IPersistenceMechanism mechanism);
		void DisconnectFrom(IPersistenceMechanism mechanism);
	}
}
