using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IColumn
	{
		ITable Table { get; }
		string Name { get; }
		bool IsRequired { get; }
		object Default { get; }
	}
}
