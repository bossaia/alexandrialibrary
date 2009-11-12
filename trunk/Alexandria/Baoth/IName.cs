using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baoth
{
	public interface IName
	{
		string Value { get; }
		int EncodingCount { get; }
		IEnumerable<INameEncoding> Encodings { get; }

	}
}
