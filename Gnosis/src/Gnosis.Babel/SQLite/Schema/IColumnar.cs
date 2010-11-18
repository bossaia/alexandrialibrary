using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnar
    {
        IColumnName Column(string name);
        IColumnName Column(string name, string type);
        IColumnName Column(string name, string type, object defaultValue);
    }

    public interface IColumnar<T>
    {
        IColumnName<T> Column(Expression<Func<T, object>> expression);
        IColumnName<T> Column(Expression<Func<T, object>> expression, string type);
        IColumnName<T> Column(Expression<Func<T, object>> expression, string type, object defaultValue);
    }
}
