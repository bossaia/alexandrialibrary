using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyReferenceConstraint : Statement, IForeignKeyReferenceConstraint
    {
        public IForeignKeyReference Column(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class ForeignKeyReferenceConstraint<T, TR> : Statement, IForeignKeyReferenceConstraint<T, TR>
    {
        public IForeignKeyReference<T, TR> Column(Expression<Func<TR, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
