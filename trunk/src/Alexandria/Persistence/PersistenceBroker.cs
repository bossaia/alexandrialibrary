using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Alexandria.Persistence
{
	public class PersistenceBroker : IPersistenceBroker
	{
		#region Constructors
		public PersistenceBroker()
		{
		}
		#endregion
	
		#region Private Fields		
		private Dictionary<Guid, ConstructorInfo> constructors = new Dictionary<Guid,ConstructorInfo>();
		#endregion
		
		#region Private Methods
		#endregion
	
		#region IPersistenceBroker Members
		public T LookupRecord<T>(Guid id) where T : IPersistentRecord
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SaveRecord(IPersistentRecord record)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void DeleteRecord(IPersistentRecord record)
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
