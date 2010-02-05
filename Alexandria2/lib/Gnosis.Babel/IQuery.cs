using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Babel
{
	public interface IQuery<T>
	{
		IQuery<T> And { get; }
		IQuery<T> Or { get; }
		IQuery<T> Where(Expression<Func<T, object>> expression);
	}
}
