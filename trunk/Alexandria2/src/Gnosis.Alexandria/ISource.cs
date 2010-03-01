using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ISource<Q,R>
		where Q : IMessage
		where R : IMessage
	{
		R Ask(Q query);
	}
}
