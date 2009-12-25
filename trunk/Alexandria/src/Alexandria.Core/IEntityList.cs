using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public interface IEntityList<T>
		: IEnumerable<T>
		where T : IEntity
	{
		int Count { get; }
	}
}
