using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public class Column
        : IColumn
    {
        public Column(string name, Type type, object @default)
        {
            _name = name;
            _type = type;
            _default = @default;
        }

        private string _name;
        private Type _type;
        private object _default;

        #region IColumn Members

        public string Name
        {
            get { return _name; }
        }

        public Type Type
        {
            get { return _type; }
        }

        public object Default
        {
            get { return _default; }
        }

        public string GetDatabaseType()
        {
            return null;
        }

        #endregion
    }
}
