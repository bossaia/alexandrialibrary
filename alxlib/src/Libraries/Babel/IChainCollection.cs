using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IChainCollection :
		IResourceCollection,
		IEnumerable<KeyValuePair<string, IChain>>
	{
		new IChain this[string name] { get; set; }
		void Add(IChain item);
		void Remove(IChain item);
		new IChainCollection GetChangedItems();
	}
}
