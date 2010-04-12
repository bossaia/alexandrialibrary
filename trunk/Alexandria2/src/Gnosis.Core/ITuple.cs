using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITuple<T> :
        IEnumerable<T>
    {
        Func<T, T, bool> EqualityFunction { get; }
        int Count { get; }

        bool Contains(T item);
        ITuple<T> Add(T item);
        ITuple<T> Remove(T item);
    }
}
