﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaSummary
    {
        IEnumerable<ITag> Tags { get; }
        IImage Thumbnail { get; }
    }
}
