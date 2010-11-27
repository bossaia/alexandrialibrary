using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public class InsertColumn : InsertColumnar, IInsertColumn
    {
        private const string KeywordSelectAll = "select all";
        private const string KeywordSelectDistinct = "select distinct";
        private const string KeywordValues = "values";

        public ISelect SelectAll
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectAll); }
        }

        public ISelect SelectDistinct
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectDistinct); }
        }

        public IInsertValue Values()
        {
            return AppendClause<IInsertValue, InsertValue>(KeywordValues);
        }

        public IStatement Values(IEnumerable<Tuple<string, object>> values)
        {
            AppendClause(KeywordValues);

            foreach (var pair in values)
                AppendParentheticalListItem(pair.Item1, pair.Item2.AsPersistentValue());

            return this;
        }
    }

    public class InsertColumn<T> : InsertColumnar<T>, IInsertColumn<T>
    {
        private const string KeywordSelectAll = "select all";
        private const string KeywordSelectDistinct = "select distinct";
        private const string KeywordValues = "values";

        public ISelect SelectAll
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectAll); }
        }

        public ISelect SelectDistinct
        {
            get { return AppendClause<ISelect, Select>(KeywordSelectDistinct); }
        }

        public IInsertValue<T> Values()
        {
            return AppendClause<IInsertValue<T>, InsertValue<T>>(KeywordValues);
        }

        public IStatement Values(IEnumerable<Expression<Func<T, object>>> expressions, T model)
        {
            AppendClause(KeywordValues);

            foreach (var expression in expressions)
                AppendParentheticalListItem(expression.ToName(), expression.GetValue(model).AsPersistentValue());

            return this;
        }
    }
}
