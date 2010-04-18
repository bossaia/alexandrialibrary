using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IChangeSet
    {
        string Entity { get; }
        object Id { get; }
        IMap<string, object> Changes { get; }
        ITuple<IChangeSet> Children { get; }
    }
}
