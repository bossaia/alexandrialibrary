﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public interface IXspfImage
        : IXspfElement
    {
        Uri Content { get; }
    }
}
