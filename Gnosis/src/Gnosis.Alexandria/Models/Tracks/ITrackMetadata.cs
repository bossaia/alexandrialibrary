﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackMetadata : IModel
    {
        ITrack Track { get; }
        string TextEncoding { get; }
        string Description { get; }
        string Content { get; }
    }
}
