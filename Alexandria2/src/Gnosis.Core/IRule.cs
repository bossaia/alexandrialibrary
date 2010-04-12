using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRule<T>
    {
        string Name { get; }
        Predicate<T> Predicate { get; }
    }
}
