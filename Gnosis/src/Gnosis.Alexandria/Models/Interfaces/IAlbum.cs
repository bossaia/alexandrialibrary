using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IAlbum : INamed, IDated, INational, ICreated, INoted
    {
        IEnumerable<IAlbumTrack> Tracks { get; }
        void AddTrack(IAlbumTrack track);
        void RemoveTrack(IAlbumTrack track);
    }
}
