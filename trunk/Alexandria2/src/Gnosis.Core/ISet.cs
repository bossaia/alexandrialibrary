using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISet<T> :
        IEnumerable<T>
    {
        Func<T, T, bool> EqualityFunction { get; }
        int Count { get; }

        bool Contains(T item);
        ISet<T> Add(T item);
        ISet<T> Remove(T item);
    }
}
