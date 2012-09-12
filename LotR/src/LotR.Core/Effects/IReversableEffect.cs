﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects
{
    public interface IReversableEffect
        : IExecutableEffect
    {
        void Undo();
    }
}
