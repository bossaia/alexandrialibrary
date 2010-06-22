using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IEvent
        : IEntity, INamed, IRenamable, IMutable
    {
        EventType Type { get; }
        DateTime Date { get; }
        IEnumerable<IFeature> Features();
        IEnumerable<IParticipation> Participations();
        IEnumerable<IEventAssociation> Associations();
        IEnumerable<IEventLink> Links();
        IEnumerable<IEventTag> Tags();

        void ChangeType(EventType type);
        void ChangeDate(DateTime date);
        void AddFeature(IFeature feature);
        void RemoveFeature(IFeature feature);
        void AddParticipation(IParticipation participation);
        void RemoveParticipation(IParticipation participation);
        void AddAssociation(IEventAssociation association);
        void RemoveAssociation(IEventAssociation association);
        void AddLink(IEventLink link);
        void RemoveLink(IEventLink link);
        void AddTag(IEventTag tag);
        void RemoveTag(IEventTag tag);
    }
}
