using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface IEntity
    {
        long Id { get; }
        bool IsChanged { get; }
        bool IsNew { get; }

        IChangeSet GetChanges();
    }
}
