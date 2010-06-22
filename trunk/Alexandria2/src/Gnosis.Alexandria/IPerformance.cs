using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IPerformance
        : IEntity
    {
        PerformanceType Type { get; }
        IWork Work { get; }

        IEnumerable<IParticipation> Participations();
        IEnumerable<IPerformanceAssociation> Associations();
        IEnumerable<IPerformanceLink> Links();
        IEnumerable<IPerformanceTag> Tags();

        void ChangeType(PerformanceType type);
        void ChangeWork(IWork work);
        void AddParticipation(IParticipation participation);
        void RemoveParticipation(IParticipation participation);
        void AddAssociation(IPerformanceAssociation association);
        void RemoveAssociation(IPerformanceAssociation association);
        void AddLink(IPerformanceLink link);
        void RemoveLink(IPerformanceLink link);
        void AddTag(IPerformanceTag tag);
        void RemoveTag(IPerformanceTag tag);
    }
}
