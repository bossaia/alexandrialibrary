using System;
using Babel.Events;

namespace Babel
{
	public interface IAggregate :
		IResource
	{
		IElementMap Elements { get; }
		bool IsHidden { get; }
		bool IsRenamed { get; }
		bool IsValid { get; }
		void BindToHidden(EventHandler<ResourceHiddenEventArgs> handler);
		void BindToRenamed(EventHandler<ResourceRenamedEventArgs> handler);
		void Hide();
		void Rename(IName name);
		void Validate();
	}
}
