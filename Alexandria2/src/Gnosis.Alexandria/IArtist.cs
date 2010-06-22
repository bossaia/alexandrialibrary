using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IArtist
        : IEntity, INamed, IRenamable, IMutable
    {
        ArtistType Type { get; }
        IEnumerable<IAppearance> Appearances();
        IEnumerable<IParticipation> Participations();
        IEnumerable<IArtistAssociation> Associations();
        IEnumerable<IArtistLink> Links();
        IEnumerable<IArtistTag> Tags();

        void ChangeType(ArtistType type);
        void AddAppearance(IAppearance appearance);
        void RemoveAppearance(IAppearance appearance);
        void AddParticipation(IParticipation participation);
        void RemoveParticipation(IParticipation participation);
        void AddAssociation(IArtistAssociation association);
        void RemoveAssociation(IArtistAssociation association);
        void AddLink(IArtistLink link);
        void RemoveLink(IArtistLink link);
        void AddTag(IArtistTag tag);
        void RemoveTag(IArtistTag tag);
    }
}
