using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IChangeGraph
		: IMessage
	{
		string Entity { get; }
		long Id { get; }
		ChangeFunction Function { get; }
		IMap<string, IChange> Changes { get; }
		IEnumerable<IChangeGraph> Children { get; }
	}
}
