using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyConstraint : Statement, IForeignKeyConstraint
    {
        public IForeignKeyColumn Column(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class ForeignKeyConstraint<T> : Statement, IForeignKeyConstraint<T>
    {
        public IForeignKeyColumn<T> Column(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
