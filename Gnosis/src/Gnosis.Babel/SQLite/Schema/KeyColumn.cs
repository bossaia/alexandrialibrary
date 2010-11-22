using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class KeyColumn : Statement, IKeyColumn
    {
        private const string KeywordAsc = "asc";
        private const string KeywordCheck = "check";
        private const string KeywordDesc = "desc";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

        public IKeyColumn ColumnAsc(string name)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IKeyColumn, KeyColumn>(KeywordAsc);
        }

        public IKeyColumn ColumnDesc(string name)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IKeyColumn, KeyColumn>(KeywordDesc);
        }

        public ITableConstraint CheckTable(string expression)
        {
            AppendParentheticalListItem(KeywordCheck);
            return AppendParentheticalSubListItem<ITableConstraint, TableConstraint>(expression);
        }

        public IKeyConstraint PrimaryKey
        {
            get { return AppendParentheticalListItem<IKeyConstraint, KeyConstraint>(KeywordPrimaryKey); }
        }

        public IKeyConstraint UniqueKey
        {
            get { return AppendParentheticalListItem<IKeyConstraint, KeyConstraint>(KeywordUniqueKey); }
        }

        public IForeignKeyConstraint ForeignKey
        {
            get { return AppendParentheticalListItem<IForeignKeyConstraint, ForeignKeyConstraint>(KeywordForeignKey); }
        }
    }

    public class KeyColumn<T> : Statement, IKeyColumn<T>
    {
        public IKeyColumn<T> ColumnAsc(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IKeyColumn<T> ColumnDesc(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ITableConstraint<T> CheckTable(string expression)
        {
            throw new NotImplementedException();
        }

        public IKeyConstraint<T> PrimaryKey
        {
            get { throw new NotImplementedException(); }
        }

        public IKeyConstraint<T> UniqueKey
        {
            get { throw new NotImplementedException(); }
        }

        public IForeignKeyConstraint<T> ForeignKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}
