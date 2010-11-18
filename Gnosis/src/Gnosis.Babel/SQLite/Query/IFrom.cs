using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IFrom : IStatement, IGroupable, IOrderable, ICompoundable, IJoinable
    {
        IPredicate<IWhere> Where(string expression);
        IPredicate<IWhere> Where<T>(Expression<Func<T, object>> expression);
    }
}
