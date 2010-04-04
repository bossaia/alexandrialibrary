using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface ILinked
    {
        IEnumerable<ILink> Links { get; }
        void AddLink(ILink link);
        void RemoveLink(ILink link);
    }
}
