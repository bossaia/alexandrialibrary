﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Yahoo
{
    public interface IMediaRating
        : IOptionalMediaRssElement
    {
        Uri Scheme { get; }
        string Content { get; }
    }
}
