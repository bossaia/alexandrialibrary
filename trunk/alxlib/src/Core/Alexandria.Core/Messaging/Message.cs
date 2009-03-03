using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Messaging
{
	public class Message
	{
		public Message(Uri messageType, object content)
		{
			this.messageType = messageType;
			this.content = content;
		}

		private Uri messageType;
		private object content;
		private MessageStatus status = MessageStatus.None;
		private Exception error;
		private IList<Message> messages = new List<Message>();

		public Uri MessageType
		{
			get { return messageType; }
		}

		public object Content
		{
			get { return content; }
		}

		public MessageStatus Status
		{
			get { return status; }
			set { status = value; }
		}

		public Exception Error
		{
			get { return error; }
			set { error = value; }
		}

		public IList<Message> Messages
		{
			get { return messages; }
		}
	}
}
