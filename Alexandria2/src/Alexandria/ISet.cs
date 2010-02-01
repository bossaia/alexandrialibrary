using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface ISet<T>
		: IEnumerable<T>
		where T : IEquatable<T>
	{
	}
}
