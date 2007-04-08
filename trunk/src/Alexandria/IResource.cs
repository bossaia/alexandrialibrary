using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IResource : IProxy
	{
		IIdentifier Id { get; }
		ILocation Location { get; }
		IFormat Format { get; }
	}
}
