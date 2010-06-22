using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IAppearance
        : IRelationship<IArtist, IEvent>
    {
        AppearanceType Type { get; }
    }
}
