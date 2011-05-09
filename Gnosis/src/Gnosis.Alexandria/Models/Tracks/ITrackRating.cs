﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackRating : IValue
    {
        byte Rating { get; }
        Uri User { get; }
        ulong PlayCount { get; }
    }
}
