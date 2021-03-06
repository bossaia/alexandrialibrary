﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeGender
        : Element, IYouTubeGender
    {
        public YouTubeGender(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeGenderContent Content
        {
            get { return GetContentEnum<YouTubeGenderContent>(YouTubeGenderContent.unspecified); }
        }
    }
}
