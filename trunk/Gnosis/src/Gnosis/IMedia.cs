﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMedia
    {
        Uri Location { get; }
        IContentType Type { get; }

        void Load();

        IEnumerable<ILink> GetLinks();
        IEnumerable<ITag> GetTags();
    }
}
