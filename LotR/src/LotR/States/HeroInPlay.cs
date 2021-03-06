﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Heroes;
using LotR.Effects;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public class HeroInPlay
        : CharacterInPlay<IHeroCard>, IHeroInPlay
    {
        public HeroInPlay(IGame game, IHeroCard card)
            : base(game, card)
        {
        }

        IHeroCard ICardInPlay<IHeroCard>.Card
        {
            get { return Card as IHeroCard; }
        }

        public override bool CanPayFor(ICostlyCard costlyCard)
        {
            if (costlyCard == null)
                throw new ArgumentNullException("costlyCard");

            if (base.CanPayFor(costlyCard))
                return true;

            if (costlyCard.PrintedSphere == Sphere.Neutral)
                return true;

            return HasResourceIcon(costlyCard.PrintedSphere);
        }

        public override bool CanPayFor(ICardEffect cardEffect)
        {
            if (cardEffect == null)
                throw new ArgumentNullException("cardEffect");

            if (base.CanPayFor(cardEffect))
                return true;

            var costlyEffect = cardEffect as ICostlyEffect;
            if (costlyEffect != null)
            {
                if (costlyEffect.ResourceSphere == Sphere.Neutral)
                    return true;

                return HasResourceIcon(costlyEffect.ResourceSphere);
            }

            return true;
        }

        public bool HasResourceIcon(Sphere sphere)
        {
            if (Card.PrintedSphere == sphere)
                return true;

            var check = new CheckForResourceIcon(Game, this, sphere);

            foreach (var attachment in Attachments.Where(x => x.HasEffect<IDuringCheckForResourceIcon>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForResourceIcon>())
                {
                    effect.DuringCheckForResourceIcon(check);
                }
            }

            return check.HasResourceIcon;
        }

        public override bool HasTrait(Trait trait)
        {
            if (base.HasTrait(trait))
                return true;

            var check = new CheckForTrait(Game, this, trait);

            foreach (var attachment in Attachments.Where(x => x.Card.HasEffect<IDuringCheckForTrait>()))
            {
                foreach (var effect in attachment.Card.Text.Effects.OfType<IDuringCheckForTrait>())
                {
                    effect.DuringCheckForTrait(check);
                }
            }

            return check.HasTrait;
        }
    }
}
