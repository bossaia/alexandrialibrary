using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IMap<K, V>
        : IEnumerable<KeyValuePair<K, V>>
    {
        V this[K key] { get; }

        int Count { get; }

        bool ContainsKey(K key);
        bool ContainsValue(V value);
    }
}
