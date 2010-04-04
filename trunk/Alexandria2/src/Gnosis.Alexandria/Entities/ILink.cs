using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface ILink :
        IEntity,
        INamed,
        ITagged
    {
        string LinkType { get; }
        Uri Path { get; }
        string MediaType { get; }
    }
}
