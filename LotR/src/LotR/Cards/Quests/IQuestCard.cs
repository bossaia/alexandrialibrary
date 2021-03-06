﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Quests
{
    public interface IQuestCard
        : IProgressableCard
    {
        ScenarioCode ScenarioCode { get; }
        IEnumerable<EncounterSet> EncounterSets { get; }
        byte Sequence { get; }
    }
}
