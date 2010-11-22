using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public class Table : Statement, ITable
    {
        const string KeywordAs = "as";

        public ISelect AsSelect
        {
            get { return AppendClause<ISelect, Select>(KeywordAs); }
        }

        public IColumnName Column(string name)
        {
            return AppendListItem<IColumnName, ColumnName>(name);
        }

        public IColumnType Column(string name, string type)
        {
            throw new NotImplementedException();
        }

        public IColumnType Column(string name, string type, object defaultValue)
        {
            throw new NotImplementedException();
        }
    }

    public class Table<T> : Statement, ITable<T>
    {
        const string KeywordAs = "as";

        public ISelect AsSelect
        {
            get { return AppendClause<ISelect, Select>(KeywordAs); }
        }

        public IColumnName<T> Column(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IColumnType<T> Column(Expression<Func<T, object>> expression, string type)
        {
            throw new NotImplementedException();
        }

        public IColumnType<T> Column(Expression<Func<T, object>> expression, string type, object defaultValue)
        {
            throw new NotImplementedException();
        }
    }

}
