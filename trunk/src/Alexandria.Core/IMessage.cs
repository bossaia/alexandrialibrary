using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telesophy.Alexandria.Core
{
	public interface IMessage
	{
		string Type { get; }
		Capabilities Capabilities { get; }
		object Data { get; }
	}
}
