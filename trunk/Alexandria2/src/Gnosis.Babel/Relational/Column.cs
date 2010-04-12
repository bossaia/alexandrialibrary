using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public class Column :
        IColumn
    {
        public Column(ITable table, string name, Type dataType, object @default)
        {
            _table = table;
            _name = name;
            _dataType = dataType;
            _default = @default;
        }

        private ITable _table;
        private string _name;
        private Type _dataType;
        private object _default;

        #region IColumn Members

        public ITable Table
        {
            get { return _table; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Type DataType
        {
            get { return _dataType; }
        }

        public object Default
        {
            get { return _default; }
        }

        public void SetTable(ITable table)
        {
            _table = table;
        }

        #endregion
    }
}
