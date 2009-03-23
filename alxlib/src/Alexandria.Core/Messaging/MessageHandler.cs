using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Messaging
{
	public abstract class MessageHandler
	{
		public abstract void HandleMessage(object sender, Message message);
	}
}
