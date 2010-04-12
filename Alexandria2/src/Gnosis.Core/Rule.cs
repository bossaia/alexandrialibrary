using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Rule<T> :
        IRule<T>
    {
        public Rule(string name, Predicate<T> predicate)
        {
            _name = name;
            _predicate = predicate;
        }

        private string _name;
        private Predicate<T> _predicate;

        #region IRule<T> Members

        public string Name
        {
            get { return _name; }
        }

        public Predicate<T> Predicate
        {
            get { return _predicate; }
        }

        #endregion
    }
}
