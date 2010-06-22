using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IParticipation
        : IRelationship<IArtist, IPerformance>
    {
        ParticipationType Type { get; }
    }
}
