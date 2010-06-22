using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IAssociation<T>
        where T : IEntity
    {
        T Parent { get; }
        T Child { get; }
        int Number { get; }
    }
}
