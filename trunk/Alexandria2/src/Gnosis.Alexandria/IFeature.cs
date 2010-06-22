using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IFeature
        : IRelationship<IEvent, IPerformance>
    {
        FeatureType Type { get; }
    }
}
