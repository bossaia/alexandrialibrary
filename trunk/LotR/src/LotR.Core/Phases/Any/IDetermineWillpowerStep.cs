﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Any
{
    public interface IDetermineWillpowerStep
        : IPhaseStep
    {
        IWillpowerfulCard Source { get; }
        byte Willpower { get; set; }
    }
}
