using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Map<K, V> :
        IMap<K, V>
    {
        public Map(IDictionary<K, V> items)
        {
            if (items != null)
                _dict = items;
        }

        private IDictionary<K, V> _dict = new Dictionary<K, V>();

        #region IMap<K,V> Members

        public V this[K key]
        {
            get { return _dict[key]; }
        }

        public int Count
        {
            get { return _dict.Count; }
        }

        public IEnumerable<K> Keys
        {
            get { return _dict.Keys; }
        }

        public IEnumerable<V> Values
        {
            get { return _dict.Values; }
        }

        public bool ContainsKey(K key)
        {
            return _dict.ContainsKey(key);
        }

        public bool ContainsValue(V value)
        {
            return _dict.Values.Contains(value);
        }

        public IMap<K, V> Add(K key, V value)
        {
            IDictionary<K, V> items = new Dictionary<K, V>(_dict);

            items.Add(key, value);

            return new Map<K, V>(items);
        }

        public IMap<K, V> Remove(K key)
        {
            IDictionary<K, V> items = new Dictionary<K, V>(_dict);

            items.Remove(key);

            return new Map<K, V>(items);
        }

        #endregion

        #region IEnumerable<KeyValuePair<K,V>> Members

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion
    }
}
