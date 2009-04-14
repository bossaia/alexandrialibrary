using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Persistence
{
	public interface IFilter
	{
		FilterType Type { get; }
		string Name { get; }
		Operator Operator { get; }
		object Value { get; }
		IFilter Child { get; }
	}
}
