using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IName :
		IEquatable<IName>
	{
		string Value { get; }
		string Disambiguation { get; }
		string Hash { get; }
	}
}
