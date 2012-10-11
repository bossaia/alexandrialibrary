﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases
{
    public interface IPhase
    {
        IRound Round { get; }
        string Name { get; }

        IPhaseStep CurrentStep { get; }
    }
}
