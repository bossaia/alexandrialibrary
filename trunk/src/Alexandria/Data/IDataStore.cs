using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Data
{
	public interface IDataStore
	{
		void Initialize(Type type);
		T Lookup<T>(Guid id) where T : class, IPersistant;
		void Save(IPersistant record);
		void Delete(IPersistant record);
	}
}
