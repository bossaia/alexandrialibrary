﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface ICardInPlay<T>
        : IState
        where T : ICard
    {
        T Card { get; }
        string Title { get; }

        bool HasTrait(Trait trait);
    }
}
