using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertValued
    {
        IInsertValue Value(string name, object value);
        IInsertValue Values(IEnumerable<Tuple<string,object>> values);
    }

    public interface IInsertValued<T>
    {
        IInsertValue<T> Value(Expression<Func<T, object>> expression, object value);
        IInsertValue<T> Values(IEnumerable<Expression<Func<T, object>>> expressions, T model);
    }
}
