using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.Domain;

namespace Gnosis.Alexandria.Entities
{
    public interface ITag :
        IEntity,
        INamed
    {
        string TagType { get; }
    }
}
