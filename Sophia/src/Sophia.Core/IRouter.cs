using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sophia.Core
{
	public interface IRouter
		: IReceiver
	{
		IEnumerable<INode> Nodes { get; }
	}
}
