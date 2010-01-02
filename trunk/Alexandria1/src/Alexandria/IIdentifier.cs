using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IIdentifier : IEquatable<IIdentifier>
	{
		Uri Namespace { get; }
		Uri Value { get; }
	}
}
