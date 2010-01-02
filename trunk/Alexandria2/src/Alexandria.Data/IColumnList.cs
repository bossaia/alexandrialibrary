using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IColumnList
		: IEnumerable<IColumn>
	{
		IColumn this[string name] { get; }
		int Count { get; }
	}
}
