using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdateColumnar
    {
        IUpdateColumn ColumnAndValue(string name, object value);
        IUpdateColumn ColumnsAndValues(IEnumerable<Tuple<string, object>> items);
    }

    public interface IUpdateColumnar<T>
    {
        IUpdateColumn<T> ColumnAndValue(Expression<Func<T, object>> name, object value);
        IUpdateColumn<T> ColumnsAndValues(IEnumerable<Expression<Func<T, object>>> names, T model);
    }
}
