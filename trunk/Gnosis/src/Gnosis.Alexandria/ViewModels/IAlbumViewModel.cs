﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAlbumViewModel
        : IMediaItemViewModel
    {
        Uri Album { get; }
        string Title { get; }
        Uri Artist { get; }
        string ArtistName { get; }
        string Year { get; }
        string Bio { get; }
        object Image { get; }
        IEnumerable<ITrackViewModel> Tracks { get; }

        void AddTrack(ITrackViewModel track);
        void RemoveTrack(ITrackViewModel track);
    }
}
