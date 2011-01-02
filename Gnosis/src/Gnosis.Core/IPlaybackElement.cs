using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IPlaybackElement
    {
        string Identifier { get; set; }
        PlaybackFunction Function { get; set; }
        StreamingStatus StreamingStatus { get; set; }
    }
}
