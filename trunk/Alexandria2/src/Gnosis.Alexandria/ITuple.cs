using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface ITuple
        : IEnumerable<KeyValuePair<string, object>>
    {
        int Count { get; }
        ITuple Add(string key, object value);
        ITuple Remove(string key);
    }
}
