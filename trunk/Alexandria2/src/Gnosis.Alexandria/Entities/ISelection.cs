using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface ISelection :
        IEntity,
        INamed,
        ITagged,
        ILinked
    {
        int Sequence { get; }
        Uri Path { get; }
        string ArtistName { get; }
        string AlbumName { get; }
        DateTime ReleaseDate { get; }
        string TrackName { get; }
        int TrackNumber { get; }
        TimeSpan Duration { get; }
    }
}
