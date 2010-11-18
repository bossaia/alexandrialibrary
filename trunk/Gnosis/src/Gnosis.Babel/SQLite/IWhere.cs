using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public interface IWhere : IStatement
    {
        IPredicate<IWhere> And<T>(Expression<Func<T, object>> expression);
        IPredicate<IWhere> Or<T>(Expression<Func<T, object>> expression);
    }
}
