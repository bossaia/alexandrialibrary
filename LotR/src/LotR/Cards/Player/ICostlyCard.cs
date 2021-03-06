﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards.Player
{
    public interface ICostlyCard
        : IPlayerCard
    {
        byte PrintedCost { get; }
        bool IsVariableCost { get; }
    }
}
