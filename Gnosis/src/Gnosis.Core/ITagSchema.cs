﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagSchema
    {
        long Id { get; }
        string Name { get; }
    }
}