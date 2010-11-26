using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public class InsertValue : Statement, IInsertValue
    {
        public IInsertValue Value(string name, object value)
        {
            return AppendParentheticalListItem<IInsertValue, InsertValue>(name, value);
        }
    }

    public class InsertValue<T> : Statement, IInsertValue<T>
    {
        public IInsertValue<T> Value(Expression<Func<T, object>> expression, object value)
        {
            return AppendParameter<IInsertValue<T>, InsertValue<T>>(expression.ToName(), value);
        }
    }
}
