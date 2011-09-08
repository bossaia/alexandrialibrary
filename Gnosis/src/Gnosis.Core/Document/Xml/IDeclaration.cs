﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public interface IDeclaration
        : INode
    {
        string Version { get; }
        ICharacterSet Encoding { get; }
        Standalone Standalone { get; }
    }
}