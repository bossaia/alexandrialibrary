using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface IPredicate<S> where S : IStatement
    {
        S IsEqualTo(string expression);
        S IsEqualTo(string expression, object value);
        S IsEqualTo<T>(Expression<Func<T, object>> expression);
        S IsEqualTo<T>(Expression<Func<T, object>> expression, object value);
        S IsNotEqualTo(string expression);
        S IsNotEqualTo(string expression, object value);
        S IsNotEqualTo<T>(Expression<Func<T, object>> expression);
        S IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value);

        S IsLessThan(string expression);
        S IsLessThan(string expression, object value);
        S IsLessThan<T>(Expression<Func<T, object>> expression);
        S IsLessThan<T>(Expression<Func<T, object>> expression, object value);
        S IsLessThanOrEqualTo(string expression);
        S IsLessThanOrEqualTo(string expression, object value);
        S IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression);
        S IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value);
        S IsGreaterThan(string expression);
        S IsGreaterThan(string expression, object value);
        S IsGreaterThan<T>(Expression<Func<T, object>> expression);
        S IsGreaterThan<T>(Expression<Func<T, object>> expression, object value);
        S IsGreaterThanOrEqualTo(string expression);
        S IsGreaterThanOrEqualTo(string expression, object value);
        S IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression);
        S IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value);

        S IsLike(string expression);
        S IsLike(string expression, object value);
        S IsLike<T>(Expression<Func<T, object>> expression);
        S IsLike<T>(Expression<Func<T, object>> expression, object value);
        S IsNotLike(string expression);
        S IsNotLike(string expression, object value);
        S IsNotLike<T>(Expression<Func<T, object>> expression);
        S IsNotLike<T>(Expression<Func<T, object>> expression, object value);

        S IsIn(string expression);
        S IsIn(string expression, object value);
        S IsNotIn(string expression);
        S IsNotIn(string expression, object value);

        S IsNull { get; }
        S IsNotNull { get; }
    }
}
