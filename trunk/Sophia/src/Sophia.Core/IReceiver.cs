using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sophia.Core
{
	public interface IReceiver
	{
		void Receive(IMessage message);
	}
}
