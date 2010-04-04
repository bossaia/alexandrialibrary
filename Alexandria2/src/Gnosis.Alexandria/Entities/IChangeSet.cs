using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface IChangeSet
    {
        long EntityId { get; }
        ChangeType ChangeType { get; }
        IEnumerable<IChange> Changes { get; }
        IEnumerable<IChangeSet> Children { get; }
    }
}
