using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnConstraint : ColumnConstrained, IColumnConstraint
    {
        public IColumnName Column(string name)
        {
            throw new NotImplementedException();
        }

        public IColumnType Column(string name, string type)
        {
            throw new NotImplementedException();
        }

        public IColumnType Column(string name, string type, object defaultValue)
        {
            throw new NotImplementedException();
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
}
