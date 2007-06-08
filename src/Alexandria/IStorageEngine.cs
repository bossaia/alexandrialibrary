using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IStorageEngine
	{
		T Lookup<T>(Guid id) where T: IPersistant;
		void Save(IPersistant record);
		void Delete(IPersistant record);
	}
}
