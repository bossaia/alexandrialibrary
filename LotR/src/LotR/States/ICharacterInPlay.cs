﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;

namespace LotR.States
{
    public interface ICharacterInPlay
        : ICardInPlay<ICharacterCard>
    {
        bool CanPayFor(ICostlyCard costlyCard);
        bool CanPayFor(ICardEffect cardEffect);
    }
}
