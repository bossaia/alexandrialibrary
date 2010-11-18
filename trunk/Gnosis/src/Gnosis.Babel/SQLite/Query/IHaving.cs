using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IHaving : IStatement, IOrderable, ICompoundable
    {
        IPredicate<IHaving> And<T>(Expression<Func<T, object>> expression);
        IPredicate<IHaving> Or<T>(Expression<Func<T, object>> expression);
    }
}
