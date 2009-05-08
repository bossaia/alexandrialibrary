using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IResource
	{
		Uri Id { get; }
		string Name{ get; }
		ILinkCollection Links { get; }
		IChainCollection Chains { get; }
		bool IsChanged { get; }
		bool IsDeleted { get; }
		bool IsRenamed { get; }
		void BindToChanged(EventHandler<ResourceChangedEventArgs> handler);
		void BindToDeleted(EventHandler<ResourceChangedEventArgs> handler);
		void BindToRenamed(EventHandler<ResourceChangedEventArgs> handler);
		void Delete();
		void Rename(string name);
		void Initialize(Uri id, string name, bool isNew);
	}
}
