using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IIdentifier : IComparable<IIdentifier>
	{
		string Value { get; }
		string Type { get; }
		IVersion Version { get; }
	}
}
