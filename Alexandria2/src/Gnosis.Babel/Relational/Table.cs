using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public class Table :
        ITable
    {
        public Table(string name, ISet<IColumn> columns, ISet<IKey> keys)
        {
            _name = name;
            _columns = columns;
            _keys = keys;
        }

        private IDatabase _database;
        private string _name;
        private ISet<IColumn> _columns;
        private ISet<IKey> _keys;

        #region ITable Members

        public IDatabase Database
        {
            get { return _database; }
        }

        public string Name
        {
            get { return _name; }
        }

        public ISet<IColumn> Columns
        {
            get { return _columns; }
        }

        public ISet<IKey> Keys
        {
            get { return _keys; }
        }

        public void SetDatabase(IDatabase database)
        {
            _database = database;
        }

        #endregion
    }
}
