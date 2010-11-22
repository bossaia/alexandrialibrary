using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnType : Statement, IColumnType
    {
        public IColumnConstraint PrimaryKeyAsc
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint PrimaryKeyAutoIncrement
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint PrimaryKeyDesc
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint NotNull
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint Unique
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint CheckColumn(string expression)
        {
            throw new NotImplementedException();
        }

        public IColumnConstraint Default(object value)
        {
            throw new NotImplementedException();
        }

        public IColumnConstraint CollateBinary
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint CollateCaseInsensitve
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint CollateRightTrim
        {
            get { throw new NotImplementedException(); }
        }

        public ITableConstrained TableConstraints
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class ColumnType<T> : Statement, IColumnType<T>
    {
        public IColumnConstraint<T> PrimaryKeyAsc
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> PrimaryKeyAutoIncrement
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> PrimaryKeyDesc
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> NotNull
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> Unique
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> CheckColumn(string expression)
        {
            throw new NotImplementedException();
        }

        public IColumnConstraint<T> Default(object value)
        {
            throw new NotImplementedException();
        }

        public IColumnConstraint<T> CollateBinary
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> CollateCaseInsensitve
        {
            get { throw new NotImplementedException(); }
        }

        public IColumnConstraint<T> CollateRightTrim
        {
            get { throw new NotImplementedException(); }
        }

        public ITableConstrained<T> TableConstraints
        {
            get { throw new NotImplementedException(); }
        }
    }
}
