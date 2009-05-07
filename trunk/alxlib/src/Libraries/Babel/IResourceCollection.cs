using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IResourceCollection : IEnumerable<IResource>
	{
		uint Count { get; }
		bool IsChanged { get; }
		void Add(IResource item);
		void Remove(IResource item);
		IResourceCollection GetChangedItems();
		void FlushChanges();
		void BindToItemAdded(EventHandler<ResourceChangedEventArgs> handler);
		void BindToItemRemoved(EventHandler<ResourceChangedEventArgs> handler);
	}
}
