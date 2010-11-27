using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public interface IFilterable
    {
        IPredicate<IWhere> Where<T>(Expression<Func<T, object>> expression);
    }

    public interface IFilterable<T>
    {
        IPredicate<IWhere> Where<TOther>(Expression<Func<TOther, object>> expression);
        IPredicate<IWhere> Where(Expression<Func<T, object>> expression);
    }
}
