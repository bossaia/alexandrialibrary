using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IAlbumTrack : INumbered
    {
        IAlbum Album { get; set; }
        ITrack Track { get; set; }
    }
}
