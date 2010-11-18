using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public interface IFilterable
    {
        IPredicate<IWhere> Where<T>(Expression<Func<T, object>> expression);
    }
}
