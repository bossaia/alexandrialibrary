﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class MicrosoftExecutable
        : IApplication
    {
        public MicrosoftExecutable(Uri location, IMediaType type)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            if (type == null)
                throw new ArgumentNullException("type");

            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IMediaType type;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
