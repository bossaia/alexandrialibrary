using System;
using System.Collections.Generic;
using Babel.Events;

namespace Babel
{
	public interface IElementMap :
		IEnumerable<KeyValuePair<string, IElement>>
	{
		IElement this[string name] { get; }
		int Count { get; }
		bool IsChanged { get; }
		bool Add(IElement item);
		void BindToItemAdded(EventHandler<ResourceMapItemAddedEventArgs> handler);
		void BindToItemChanged(EventHandler<ResourceMapItemChangedEventArgs> handler);
		void Clear();
		bool Contains(IElement item);
		bool ContainsName(string name);
		void Flush();
		IElementMap GetChangedItems();

		T GetItem<T>(string name)
			where T : IElement;
	}
}
