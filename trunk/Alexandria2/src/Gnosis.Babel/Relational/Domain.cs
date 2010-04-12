using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public class Domain :
        IDomain
    {
        public Domain(string name, Type baseType, object @default, ITuple<IRule<object>> rules)
        {
            _name = name;
            _baseType = baseType;
            _default = @default;
            _rules = rules;
        }

        private IDatabase _database;
        private string _name;
        private Type _baseType;
        private object _default;
        private ITuple<IRule<object>> _rules;

        #region IDomain Members

        public IDatabase Database
        {
            get { return _database; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Type BaseType
        {
            get { return _baseType; }
        }

        public object Default
        {
            get { return _default; }
        }

        public ITuple<IRule<object>> Rules
        {
            get { return _rules; }
        }

        public void SetDatabase(IDatabase database)
        {
            _database = database;
        }

        #endregion
    }
}
