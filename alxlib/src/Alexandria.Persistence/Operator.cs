using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Persistence
{
	public enum Operator
	{
		None = 0,
		EqualTo,
		GreaterThan,
		GreaterThanOrEqualTo,
		LessThan,
		LessThanOrEqualTo,
		Like,
		NotEqualTo,
	}
}
