using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnConstrained : Statement, IColumnConstrained
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
}
