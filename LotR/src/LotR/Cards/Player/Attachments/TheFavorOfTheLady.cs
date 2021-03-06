﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Heroes;
using LotR.States;
using LotR.Effects.Phases;

namespace LotR.Cards.Player.Attachments
{
    public class TheFavorOfTheLady
        : AttachmentCardBase
    {
        public TheFavorOfTheLady()
            : base("The Favor of the Lady", CardSet.Core, 55, Sphere.Spirit, 2)
        {
            AddTrait(Trait.Condition);
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            if (cardInPlay == null)
                throw new ArgumentNullException("cardInPlay");

            return (cardInPlay is IHeroCard);
        }
    }
}
