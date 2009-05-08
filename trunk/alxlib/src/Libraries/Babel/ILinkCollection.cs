using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ILinkCollection :
		IResourceCollection,
		IEnumerable<KeyValuePair<string, ILink>>
	{
		new ILink this[string name] { get; set; }
		void Add(ILink item);
		void Remove(ILink item);
		new ILinkCollection GetChangedItems();
	}
}
