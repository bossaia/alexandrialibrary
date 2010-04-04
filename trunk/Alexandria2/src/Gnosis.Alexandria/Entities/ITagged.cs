using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface ITagged
    {
        IEnumerable<ITag> Tags { get; }
        void AddTag(ITag tag);
        void RemoveTag(ITag tag);
    }
}
