﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlayableViewModel
    {
        object PlaybackIcon { get; }

        bool IsPaused { get; set; }
        bool IsPlaying { get; set; }
        bool IsStopped { get; set; }

        void ClearStatus();

        IPlaylistViewModel ToPlaylist(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory);
        IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, uint number);
    }
}
