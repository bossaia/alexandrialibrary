using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ISink<C>
		where C : IMessage
	{
		void Tell(C command);
	}
}
