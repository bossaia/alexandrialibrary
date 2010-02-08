using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Collections
{
	public interface ISet<T>
		: IEnumerable<T>
		where T : IEquatable<T>
	{
	}
}
