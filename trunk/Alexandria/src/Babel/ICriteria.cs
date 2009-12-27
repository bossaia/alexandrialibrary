using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Babel
{
	public interface ICriteria<T>
	{
		ICriteria<T> And { get; }
		ICriteria<T> Or { get; }
		ICriteria<T> That(Expression<Func<T, object>> expression);
	}
}
