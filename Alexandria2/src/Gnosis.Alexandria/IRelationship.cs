using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IRelationship<ParentType, ChildType>
        : IEntity
        where ParentType : IEntity
        where ChildType : IEntity
    {
        ParentType Parent { get; }
        ChildType Child { get; }
        int Number { get; }
    }
}
