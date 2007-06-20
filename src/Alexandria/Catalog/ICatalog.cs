using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Metadata;
using Alexandria.Media;

namespace Alexandria.Catalog
{
    public interface ICatalog
    {
        IUser User { get; }
        IList<IAlbum> Albumds { get; }
        IList<IPlaylist> Playlists { get; }
    }
}
