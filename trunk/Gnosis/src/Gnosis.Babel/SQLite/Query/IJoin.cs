using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IJoin : IStatement
    {
        IPredicate<IJoinCondition> On<T>(Expression<Func<T, object>> expression);
        IPredicate<IJoinCondition> On<T>(Expression<Func<T, object>> expression, string table);
        IPredicate<IJoinCondition> On(string expression);
    }
}
