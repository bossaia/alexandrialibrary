using System;
using Babel.Events;

namespace Babel
{
	public interface IAggregate :
		IResource
	{
		IResourceMap<ILink> Links { get; }
		IResourceMap<IChain> Chains { get; }
		bool IsHidden { get; }
		bool IsRenamed { get; }
		void BindToHidden(EventHandler<ResourceHiddenEventArgs> handler);
		void BindToRenamed(EventHandler<ResourceRenamedEventArgs> handler);
		bool Hide();
		bool Rename(IName name);
	}
}
