using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IAction
		: IMessage
	{
		string Name { get; }
		object Value { get; }
	}
}
