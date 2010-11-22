using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyReference : Statement, IForeignKeyReference
    {
        private const string KeywordCheck = "check";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUnique = "unique";

        public IForeignKeyReference Column(string name)
        {
            return AppendParentheticalSubListItem<IForeignKeyReference, ForeignKeyReference>(name);
        }

        public ITableConstraint CheckTable(string expression)
        {
            throw new NotImplementedException();
        }

        public IKeyConstraint PrimaryKey
        {
            get { throw new NotImplementedException(); }
        }

        public IKeyConstraint UniqueKey
        {
            get { throw new NotImplementedException(); }
        }

        public IForeignKeyConstraint ForeignKey
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class ForeignKeyReference<T, TR> : Statement, IForeignKeyReference<T, TR>
    {
        private const string KeywordCheck = "check";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUnique = "unique";

        public IForeignKeyReference<T, TR> Column(Expression<Func<TR, object>> expression)
        {
            return
                AppendParentheticalSubListItem<IForeignKeyReference<T, TR>, ForeignKeyReference<T, TR>>(
                    expression.ToName());
        }

        public ITableConstraint<T> CheckTable(string expression)
        {
            AppendParentheticalListItem(KeywordCheck);
            return AppendParentheticalSubListItem<ITableConstraint<T>, TableConstraint<T>>(expression);
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
