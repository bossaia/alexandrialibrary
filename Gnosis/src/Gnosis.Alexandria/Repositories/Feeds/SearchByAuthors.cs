﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByAuthors
        : EntitySearchBase<IFeed>
    {
        public SearchByAuthors()
            : base("Feed.Authors LIKE @Authors", x => x.Authors)
        {
        }
    }
}
