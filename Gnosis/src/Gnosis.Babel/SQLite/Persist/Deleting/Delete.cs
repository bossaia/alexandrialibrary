using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Deleting
{
    #region Delete

    public class Delete : Statement, IDelete
    {
        public IDeleteFrom From(string table)
        {
            throw new NotImplementedException();
        }

        public IPredicate<IWhere> Where<T>(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Delete<T>

    public class Delete<T> : Statement, IDelete<T>
    {
        public IDeleteFrom<T> From(string table)
        {
            throw new NotImplementedException();
        }

        public IPredicate<IWhere> Where(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
