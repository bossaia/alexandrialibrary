using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IGroupBy : IStatement
    {
        IGrouping Column<T>(Expression<Func<T, object>> expression);
    }
}
