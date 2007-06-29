using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public interface IPersistentRecord
	{
		Guid Id { get; }
		IPersistenceBroker PersistenceBroker { get; set; }
		bool IsLoaded { get; }
		bool IsDirty { get; }
		DateTime TimeStamp { get; set; }
		void Load();
		void Save();
		void Delete();
	}
}
