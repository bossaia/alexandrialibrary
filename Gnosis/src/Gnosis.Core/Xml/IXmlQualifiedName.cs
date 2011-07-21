﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlQualifiedName
        : IXmlMarkup
    {
        string Prefix { get; }
        string LocalPart { get; }
    }
}
