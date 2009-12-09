using System;
using System.Collections.Generic;

namespace Sophia.Core
{
	public interface IContext
	{
		IEnumerable<INode> Nodes { get; }
		IEnumerable<IRoute> Routes { get; }
		void Send(Uri sender, IMessage message);
	}
}
