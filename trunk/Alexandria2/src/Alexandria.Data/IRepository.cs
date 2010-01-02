using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IRepository<T>
	{
		T Find(ICriteria<T> criteria);
		IEnumerable<T> List(ICriteria<T> criteria);
	}
}
