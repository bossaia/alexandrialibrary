using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Collections
{
	public interface ITuple<T>
		: IEnumerable<T>
		where T : IEquatable<T>, IComparable<T>
	{
	}
}
