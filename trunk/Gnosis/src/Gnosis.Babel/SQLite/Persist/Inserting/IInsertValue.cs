using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertValue : IStatement
    {
        IInsertValue Value(string name, object value);
    }

    public interface IInsertValue<T> : IStatement
    {
        IInsertValue<T> Value(Expression<Func<T, object>> expression, object value);
    }

}
