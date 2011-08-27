﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeOccupation
        : YouTubeSimpleContentElement, IYouTubeOccupation
    {
        public YouTubeOccupation(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}