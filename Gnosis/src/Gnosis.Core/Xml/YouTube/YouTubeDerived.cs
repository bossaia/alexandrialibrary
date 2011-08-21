﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeDerived
        : Element, IYouTubeDerived
    {
        public YouTubeDerived(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeDerivedSource Content
        {
            get { return GetContentEnum<YouTubeDerivedSource>(YouTubeDerivedSource.unspecified); }
        }
    }
}
