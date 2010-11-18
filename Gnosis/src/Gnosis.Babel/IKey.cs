using System;
using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface IKey<T>
    {
        string Name { get; }
        KeyType KeyType { get; }
        IEnumerable<Tuple<string,bool>> Fields { get; }
    }
}
