using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public class Tuple
        : ITuple
    {
        public Tuple()
        {
        }

        public Tuple(IEnumerable<KeyValuePair<string, object>> data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            foreach (KeyValuePair<string, object> item in data)
                _map.Add(item);
        }

        private IDictionary<string, object> _map = new Dictionary<string, object>();

        #region ITuple Members

        public int Count
        {
            get { return _map.Count; }
        }

        public ITuple Add(string key, object value)
        {
            var added = new Dictionary<string, object>(_map);
            added.Add(key, value);
            return new Tuple(added);
        }

        public ITuple Remove(string key)
        {
            var removed = new Dictionary<string, object>(_map);
            removed.Remove(key);
            return new Tuple(removed);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        #endregion
    }
}
