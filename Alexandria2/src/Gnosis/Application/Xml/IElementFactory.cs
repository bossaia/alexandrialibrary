﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IElementFactory
    {
        string ElementName { get; }
        bool IsValidFor(IElement element);
        IElement Create(INode parent, IQualifiedName name);
    }
}
