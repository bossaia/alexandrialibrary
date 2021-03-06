﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects
{
    public interface IQuestion
        : IChoiceItem
    {
        IPlayer Player { get; }
        IAnswer Parent { get; }
        IEnumerable<IAnswer> Answers { get; }
        uint MinimumChosenAnswers { get; }
        uint MaximumChosenAnswers { get; }

        void AddAnswer(IAnswer answer);
    }
}
