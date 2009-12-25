using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IKeyList
		: IEnumerable<IKey>
	{
		int Count { get; }
	}
}
