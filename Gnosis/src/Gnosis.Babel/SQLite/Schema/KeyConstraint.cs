using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class KeyConstraint : Statement, IKeyConstraint
    {
        public IKeyColumn ColumnAsc(string name)
        {
            throw new NotImplementedException();
        }

        public IKeyColumn ColumnDesc(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class KeyConstraint<T> : Statement, IKeyConstraint<T>
    {
        public IKeyColumn<T> ColumnAsc(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IKeyColumn<T> ColumnDesc(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
