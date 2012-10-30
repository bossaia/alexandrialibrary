﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class FollowScenarioSetup
        : FrameworkEffectBase, IDuringSetup
    {
        public FollowScenarioSetup(IGame game)
            : base("Follow Scenario Setup Instruction", game)
        {
        }

        public void DuringSetup(IGame game)
        {
            game.QuestArea.SetupScenario();
        }
    }
}
