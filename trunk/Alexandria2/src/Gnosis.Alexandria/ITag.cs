using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface ITag
        : IEntity, INamed, IMutable
    {
        TagType Type { get; }

        void ChangeType(TagType type);
    }
}
