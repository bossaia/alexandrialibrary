﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IArtist
        : IEntity
    {
        string Name { get; set; }
    }
}
