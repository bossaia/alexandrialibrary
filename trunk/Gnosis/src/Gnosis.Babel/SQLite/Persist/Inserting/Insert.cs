using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    #region Insert

    public class Insert : Statement, IInsert
    {
        public IInsertColumn Into(string table)
        {
            throw new NotImplementedException();
        }

        public IInsertConflictClause OrAbort
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause OrFail
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause OrIgnore
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause OrReplace
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause OrRollback
        {
            get { throw new NotImplementedException(); }
        }
    }

    #endregion

    #region Insert<T>

    public class Insert<T> : Statement, IInsert<T>
    {
        public IInsertColumn<T> Into(string table)
        {
            throw new NotImplementedException();
        }

        public IInsertConflictClause<T> OrAbort
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause<T> OrFail
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause<T> OrIgnore
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause<T> OrReplace
        {
            get { throw new NotImplementedException(); }
        }

        public IInsertConflictClause<T> OrRollback
        {
            get { throw new NotImplementedException(); }
        }
    }

    #endregion
}
