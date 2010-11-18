using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IIndexColumnar
    {
        IIndexColumnar ColumnAsc(string name);
        IIndexColumnar ColumnDesc(string name);
    }

    public interface IIndexColumnar<T>
    {
        IIndexColumnar<T> ColumnAsc(Expression<Func<T, object>> expression);
        IIndexColumnar<T> ColumnDesc(Expression<Func<T, object>> expression);
    }
}
