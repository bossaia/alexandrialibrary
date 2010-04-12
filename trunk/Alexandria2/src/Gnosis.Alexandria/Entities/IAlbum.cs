using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.Domain;

namespace Gnosis.Alexandria.Entities
{
    public interface IAlbum :
        IEntity,
        INamed,
        ITagged
    {
        IArtist Artist { get; }
        DateTime ReleaseDate { get; }
        string ReleaseCountry { get; }
        IEnumerable<ITrack> Tracks { get; }

        void ChangeArtist(IArtist artist);
        void ChangeReleaseDate(DateTime releaseDate);
        void ChangeReleaseCountry(string ReleaseCountry);
        void AddTrack(ITrack track);
        void RemoveTrack(ITrack track);
    }
}
