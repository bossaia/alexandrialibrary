using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IIdentifier
	{
		string Value { get; }
		string Type { get; }
	}
}
