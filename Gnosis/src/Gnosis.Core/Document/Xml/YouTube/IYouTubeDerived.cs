﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public interface IYouTubeDerived
        : IYouTubeElement
    {
        YouTubeDerivedSource Content { get; }
    }
}