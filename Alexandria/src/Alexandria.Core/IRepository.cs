using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public interface IRepository<T>
		where T : IEntity
	{
		T Find(ICriteria<T> criteria);
		IEnumerable<T> List(ICriteria<T> criteria);
	}
}
