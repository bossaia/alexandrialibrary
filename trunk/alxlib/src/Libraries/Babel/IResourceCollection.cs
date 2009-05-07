using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IResourceCollection : IEnumerable<IResource>
	{
		EventHandler<ResourceChangeEventArgs> ItemAdded { get; set; }
		EventHandler<ResourceChangeEventArgs> ItemRemoved { get; set; }
		uint GetCount();
		void Add(IResource item);
		void Remove(IResource item);
		IResourceCollection GetAddedItems();
		IResourceCollection GetRemovedItems();
	}
}
