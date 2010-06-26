using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public struct IndexedColumn
    {
        public IndexedColumn(string name)
        {
            _name = name;
            _isAscending = true;
        }

        public IndexedColumn(string name, bool isAscending)
        {
            _name = name;
            _isAscending = isAscending;
        }

        private readonly string _name;
        private readonly bool _isAscending;

        string Name { get { return _name; } }
        bool IsAscending { get { return _isAscending; } }
    }
}
