using System;
using System.Collections.Generic;
using Babel.Events;

namespace Babel
{
	public interface IResourceMap<T> :
		IEnumerable<KeyValuePair<string, T>>
		where T : IResource
	{
		T this[string name] { get; }
		int Count { get; }
		bool IsChanged { get; }
		bool Add(T item);
		void BindToItemAdded(EventHandler<ResourceMapItemAddedEventArgs> handler);
		void BindToItemChanged(EventHandler<ResourceMapItemChangedEventArgs> handler);
		void Clear();
		bool Contains(T item);
		bool ContainsName(string name);
		void Flush();
		IResourceMap<T> GetChangedItems();
	}
}
