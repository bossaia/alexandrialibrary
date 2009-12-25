using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IKey
	{
		ITable Table { get; }
		IColumnList Columns { get; }
		bool IsPrimary { get; }
		bool IsUnique { get; }
		bool IsIdentity { get; }
	}
}
