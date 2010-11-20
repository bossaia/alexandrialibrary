using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    #region Update

    public class Update : Statement, IUpdate
    {
        public IUpdateSet Table(string table)
        {
            throw new NotImplementedException();
        }

        public IUpdateConflictClause OrAbort
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause OrFail
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause OrIgnore
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause OrReplace
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause OrRollback
        {
            get { throw new NotImplementedException(); }
        }
    }

    #endregion

    #region Update<T>

    public class Update<T> : Statement, IUpdate<T>
    {
        public IUpdateSet<T> Table(string table)
        {
            throw new NotImplementedException();
        }

        public IUpdateConflictClause<T> OrAbort
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause<T> OrFail
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause<T> OrIgnore
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause<T> OrReplace
        {
            get { throw new NotImplementedException(); }
        }

        public IUpdateConflictClause<T> OrRollback
        {
            get { throw new NotImplementedException(); }
        }
    }

    #endregion
}
