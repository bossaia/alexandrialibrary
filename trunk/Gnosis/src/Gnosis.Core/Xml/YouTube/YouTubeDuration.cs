﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeDuration
        : Element, IYouTubeDuration
    {
        public YouTubeDuration(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public int Seconds
        {
            get { return GetAttributeInt32("seconds"); }
        }
    }
}