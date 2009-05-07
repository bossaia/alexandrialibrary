using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IResource
	{
		Uri Id { get; }
		Uri Schema { get; }
		string Name{ get; }
		ILinkCollection Links { get; }
		IChainCollection Chains { get; }
		bool IsHidden { get; }
		bool IsRenamed { get; }
		EventHandler<ResourceChangeEventArgs> LinkAdded { get; set; }
		EventHandler<ResourceChangeEventArgs> LinkRemoved { get; set; }
		EventHandler<ResourceChangeEventArgs> ChainAdded { get; set; }
		EventHandler<ResourceChangeEventArgs> ChainRemoved { get; set; }
		EventHandler<ResourceChangeEventArgs> Hidden { get; set; }
		EventHandler<ResourceChangeEventArgs> Renamed { get; set; }
		void Hide();
		void Rename(string name);
	}
}
