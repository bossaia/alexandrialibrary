using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface ITrack :
        IEntity,
        INamed,
        ITagged,
        ILinked
    {
        IArtist Artist { get; }
        IAlbum Album { get; }
        int TrackNumber { get; }
        TimeSpan Duration { get; }

        void ChangeArtist(IArtist artist);
        void ChangeAlbum(IAlbum album);
        void ChangeTrackNumber(int trackNumber);
        void ChangeDuration(TimeSpan duration);
    }
}
