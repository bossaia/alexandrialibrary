using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPersistant
	{
		Guid Id { get; }		
		void Save(IStorageEngine engine);
		void Delete(IStorageEngine engine);
	}
}
