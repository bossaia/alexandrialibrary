using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public interface IPersistenceBroker
	{
		T LookupRecord<T>(Guid id) where T: IPersistentRecord;
		void SaveRecord(IPersistentRecord record);
		void DeleteRecord(IPersistentRecord record);
		void ConnectTo(IPersistenceMechanism mechanism);
		void DisconnectFrom(IPersistenceMechanism mechanism);
	}
}
