using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnar
    {
        IColumnar Column(string name);
    }

    public interface IColumnar<T>
    {
        IColumnName<T> Column(Expression<Func<T, object>> expression);
    }
}
