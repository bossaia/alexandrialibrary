using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ISchema :
		IMessage
	{
		string Name { get; }
		IMap<string, IElement> Elements { get; }
	}
}
