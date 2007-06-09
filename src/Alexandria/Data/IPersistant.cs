using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Data
{
	public interface IPersistant
	{
		Guid Id { get; }
		IDataStore DataStore { get; set; }
		void Save();
		void Delete();
	}
}
