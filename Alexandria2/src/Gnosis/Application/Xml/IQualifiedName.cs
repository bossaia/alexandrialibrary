﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IQualifiedName
    {
        string Prefix { get; }
        string LocalPart { get; }
    }
}
