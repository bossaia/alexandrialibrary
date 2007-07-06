
using System;
using System.Data;

namespace Alexandria.Persistence
{
	public interface IPersistenceMechanism
	{
		string Name { get; }
		bool IsOpen { get; }
		void Open();
		void Close();
		DataTable GetDataTable(IPersistenceBroker broker, Type type);
		void FillDataTable(DataTable table, string idValue);
		object GetDatabaseValue(object value);
		object GetRecordValue(object value);
	}
}
