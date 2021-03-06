﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public interface IYouTubeState
        : IYouTubeSimpleContentElement
    {
        YouTubeStateName StateName { get; }
        YouTubeStateReasonCode ReasonCode { get; }
        Uri HelpUrl { get; }
    }
}
