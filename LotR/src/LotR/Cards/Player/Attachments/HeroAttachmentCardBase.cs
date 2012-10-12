﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;

namespace LotR.Cards.Player.Attachments
{
    public abstract class HeroAttachmentCardBase
        : AttachmentCardBase
    {
        protected HeroAttachmentCardBase(string title, CardSet cardSet, uint cardNumber, Sphere sphere, byte resourceCost, bool isUnique, bool isRestricted)
            : base(title, cardSet, cardNumber, sphere, resourceCost, isUnique, isRestricted)
        {
        }

        public override bool CanBeAttachedTo(IGameState state, ICanHaveAttachments card)
        {
            return (card is IHeroCard);
        }
    }
}
