using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Data
{
	public interface IPersistant
	{
		Guid Id { get; }
		bool IsNew { get; }
		IStorageEngine Engine { get; set; }
		void Save();
		void Delete();
	}
}
