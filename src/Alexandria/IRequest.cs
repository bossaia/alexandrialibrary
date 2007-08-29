using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IRequest : IMessage
	{
		Guid Guid { get; }
		IController Controller { get; }
		object UserState { get; }
		void UpdateController(object value, Exception exception);
	}
}
