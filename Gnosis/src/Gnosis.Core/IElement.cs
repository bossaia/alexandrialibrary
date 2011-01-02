using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IElement
    {
        ElementType Type { get; set; }
        int Sequence { get; set; }
        bool IsOptional { get; set; }
        bool IsRecursive { get; set; }

        ITrackElement Track { get; }
        IPlaybackElement Playback { get; }
        IResultElement Result { get; }
    }
}
