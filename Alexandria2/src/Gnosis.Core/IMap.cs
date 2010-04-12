using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMap<K, V>
        : IEnumerable<KeyValuePair<K, V>>
    {
        V this[K key] { get; }

        int Count { get; }
        IEnumerable<K> Keys { get; }
        IEnumerable<V> Values { get; }

        bool ContainsKey(K key);
        bool ContainsValue(V value);

        IMap<K, V> Add(K key, V value);
        IMap<K, V> Remove(K key);
    }
}
