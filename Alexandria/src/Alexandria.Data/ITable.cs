using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface ITable
	{
		string Name { get; }
		IColumnList Columns { get; }
		IKeyList Keys { get; }
		IJoinList Joins { get; } 
	}
}
