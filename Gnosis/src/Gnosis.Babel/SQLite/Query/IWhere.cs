using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IWhere : IStatement
    {
        IPredicate<IWhere> And<T>(Expression<Func<T, object>> expression);
        IPredicate<IWhere> Or<T>(Expression<Func<T, object>> expression);
        IGroupBy GroupBy { get; }
        IOrderBy OrderBy { get; }
    }
}
