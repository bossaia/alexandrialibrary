using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ISet<T> :
		IEnumerable<T>,
		IMessage
		where T : IEquatable<T>
	{
		int Count { get; }

		bool Contains(T item);
	}
}
