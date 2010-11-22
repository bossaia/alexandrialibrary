using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertValued
    {
        IInsertValue Values();
        IStatement Values(IEnumerable<Tuple<string,object>> values);
    }

    public interface IInsertValued<T>
    {
        IInsertValue<T> Values();
        IStatement Values(IEnumerable<Expression<Func<T, object>>> expressions, T model);
    }
}
