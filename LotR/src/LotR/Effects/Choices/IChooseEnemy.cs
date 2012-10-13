﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseEnemy
        : IPlayerChoice
    {
        IEnemyInPlay Enemy { get; set; }
    }
}
