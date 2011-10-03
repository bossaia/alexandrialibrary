﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILinkSchemaFactory
    {
        ILinkSchema Get(Uri identifier);

        void Add(ILinkSchema schema);
        void Remove(ILinkSchema schema);
    }
}
