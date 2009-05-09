using System;
using Babel.Events;

namespace Babel
{
	public interface IResource
	{
		Uri Id { get; }
		IName Name { get; }
		bool IsChanged { get; }
		void BindToChanged(EventHandler<ResourceChangedEventArgs> handler);
	}
}
