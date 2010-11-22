using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class TableConstrained : Statement, ITableConstrained
    {
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

    public class TableConstrained<T> : Statement, ITableConstrained<T>
    {
        public ITableConstraint<T> CheckTable(Predicate<T> check)
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
