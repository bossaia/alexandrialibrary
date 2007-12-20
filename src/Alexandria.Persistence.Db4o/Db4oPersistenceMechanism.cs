using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using com.db4o;
using Alexandria;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.Db4o
{
	public class Db4oPersistenceMechanism : IPersistenceMechanism
	{		
		#region Private Fields
		IPersistenceBroker broker;
		private ObjectContainer container;
		#endregion
	
		#region Constructors
		public Db4oPersistenceMechanism(string dbPath)
		{
			container = com.db4o.Db4o.OpenFile(dbPath);
		}
		#endregion

		#region IPersistenceMechanism Members
		public string Name
		{
			get { return "db4o Object Database"; }
		}

		public IPersistenceBroker Broker
		{
			get { return broker; }
			set { broker = value; }
		}

		public System.Data.Common.DbConnection GetConnection()
		{
			return null;
		}

		public void InitializeRecordMap(RecordMap recordMap, System.Data.Common.DbTransaction transaction)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public T LookupRecord<T>(Guid id, System.Data.Common.DbConnection connection) where T : IRecord
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SaveRecord(IRecord record, System.Data.Common.DbTransaction transaction)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void DeleteRecord(IRecord record, System.Data.Common.DbTransaction transaction)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
