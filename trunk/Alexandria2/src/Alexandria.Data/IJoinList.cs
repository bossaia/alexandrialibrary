using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IJoinList
		: IEnumerable<IJoin>
	{
		int Count { get; }
	}
}
