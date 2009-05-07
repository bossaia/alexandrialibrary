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
		void Add(IChain item);
		void Remove(IChain item);
		new IChainCollection GetAddedItems();
		new IChainCollection GetRemovedItems();
	}
}
