using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public class Where : Statement, IWhere
    {
        public IPredicate<IWhere> And<T>(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicate<IWhere> Or<T>(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
