﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface INational : IModel
    {
        ICountry Country { get; set; }
    }
}