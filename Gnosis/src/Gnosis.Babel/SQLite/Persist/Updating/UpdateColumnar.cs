using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public class UpdateColumnar : Statement, IUpdateColumnar
    {
        private const string KeywordAssign = "=";

        public IUpdateColumn ColumnAndValue(string name, object value)
        {
            AppendListItem(name);
            AppendWord(KeywordAssign);
            return AppendParameter<IUpdateColumn, UpdateColumn>(name, value);
        }

        public IUpdateColumn ColumnsAndValues(IEnumerable<Tuple<string, object>> items)
        {
            foreach (var item in items)
            {
                AppendListItem(item.Item1);
                AppendWord(KeywordAssign);
                AppendParameter(item.Item1, item.Item2);
            }

            return Transform<IUpdateColumn, UpdateColumn>();
        }
    }

    public class UpdateColumnar<T> : Statement, IUpdateColumnar<T>
    {
        private const string KeywordAssign = "=";

        public IUpdateColumn<T> ColumnAndValue(Expression<Func<T, object>> expression, object value)
        {
            AppendListItem(expression.ToName());
            AppendWord(KeywordAssign);
            return AppendParameter<IUpdateColumn<T>, UpdateColumn<T>>(expression.ToName(), value);
        }

        public IUpdateColumn<T> ColumnsAndValues(IEnumerable<Expression<Func<T, object>>> expressions, T model)
        {
            foreach (var expression in expressions)
            {
                AppendListItem(expression.ToName());
                AppendWord(KeywordAssign);
                AppendParameter(expression.ToName(), expression.GetValue(model).AsPersistentValue());
            }

            return Transform<IUpdateColumn<T>, UpdateColumn<T>>();
        }
    }
}
