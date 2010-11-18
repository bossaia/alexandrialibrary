using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IGrouping : IStatement, IOrderable, IGroupBy
    {
        IPredicate<IHaving> Having<T>(Expression<Func<T, object>> expression);
    }
}
