using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Alexandria.Core
{
	public interface ICriteria<T>
		where T : IEntity
	{
		ICriteria<T> And<V>(Expression<Func<T, V>> expression, V value);
		ICriteria<T> Or<V>(Expression<Func<T, V>> expression, V value);

		//ICriteria<T> IsEqualTo(object value);
		//ICriteria<T> IsGreaterThan(object value);
		//ICriteria<T> IsGreaterThanOrEqualTo(object value);
		//ICriteria<T> IsLessThan(object value);
		//ICriteria<T> IsLessThanOrEqualTo(object value);
		//ICriteria<T> IsLike(object value);
		//ICriteria<T> IsNotEqualTo(object value);
		//ICriteria<T> IsNotLike(object value);
		//ICriteria<T> IsNotNull { get; }
		//ICriteria<T> IsNull { get; }

		//bool IsValid();
	}
}
