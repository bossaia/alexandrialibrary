using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abraxas
{
	public interface IEntityMap<T>
		: IEnumerable<T>
		where T : IEntity
	{
		IEntity this[string hash] { get; }
		int Count { get; }

		bool ContainsHash(string hash);
		bool IsUnique(string hash);
	}
}
