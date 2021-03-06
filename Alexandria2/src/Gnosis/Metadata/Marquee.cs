﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class Marquee
        : IMarquee
    {
        public Marquee(Uri location, MetadataCategory category, string name, string subtitle)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (name == null)
                throw new ArgumentNullException("name");
            if (subtitle == null)
                throw new ArgumentNullException("subtitle");

            this.location = location;
            this.category = category;
            this.name = name;
            this.subtitle = subtitle;
        }

        private Uri location;
        private MetadataCategory category = MetadataCategory.None;
        private string name;
        private string subtitle;

        public Uri Location
        {
            get { return location; }
        }

        public MetadataCategory Category
        {
            get { return category; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Subtitle
        {
            get { return subtitle; }
        }
    }
}
