﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public interface IParameter
    {
        string Name { get; }
        object Value { get; }
    }
}