﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public interface IRssTextInput
        : IElement
    {
        string Title { get; }
        string Description { get; }
        string InputName { get; }
        Uri Link { get; }
    }
}
