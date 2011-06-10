﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class LookupByLocation :
        LookupBase<IFeed>
    {
        public LookupByLocation()
            : base("Feed.Location = @Location", x => x.Location)
        {
        }
    }
}
