﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Phases.Any;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public abstract class CharacterInPlay<T>
        : PlayerCardInPlay<T>, ICharacterInPlay, IAttachmentHostInPlay, IWillpowerfulInPlay, IAttackingInPlay, IDefendingInPlay
        where T : ICharacterCard, IAttachmentHostCard
    {
        public CharacterInPlay(IGame game, T card)
            : base(game, card)
        {
            this.characterCard = card as ICharacterCard;
            willpower = characterCard.PrintedWillpower;
            attack = characterCard.PrintedAttack;
            defense = characterCard.PrintedDefense;
            hitPoints = characterCard.PrintedHitPoints;
        }

        private readonly IDictionary<Guid, IAttachableInPlay> attachments = new Dictionary<Guid, IAttachableInPlay>();
        private byte willpower;
        private byte attack;
        private byte defense;
        private byte hitPoints;

        private readonly ICharacterCard characterCard;

        ICharacterCard ICardInPlay<ICharacterCard>.Card
        {
            get { return characterCard; }
        }

        IAttachmentHostCard ICardInPlay<IAttachmentHostCard>.Card
        {
            get { return characterCard; }
        }

        IWillpowerfulCard ICardInPlay<IWillpowerfulCard>.Card
        {
            get { return characterCard; }
        }

        IAttackingCard ICardInPlay<IAttackingCard>.Card
        {
            get { return characterCard; }
        }

        IDefendingCard ICardInPlay<IDefendingCard>.Card
        {
            get { return characterCard; }
        }

        public IEnumerable<IAttachableInPlay> Attachments
        {
            get { return attachments.Values; }
        }

        public byte Willpower
        {
            get { return willpower; }
            set
            {
                if (willpower == value)
                    return;

                willpower = value;
                OnPropertyChanged("Willpower");
            }
        }

        public byte Attack
        {
            get { return attack; }
            set
            {
                if (attack == value)
                    return;

                attack = value;
                OnPropertyChanged("Attack");
            }
        }

        public byte Defense
        {
            get { return defense; }
            set
            {
                if (defense == value)
                    return;

                defense = value;
                OnPropertyChanged("Defense");
            }
        }

        public byte HitPoints
        {
            get { return hitPoints; }
            set
            {
                if (hitPoints == value)
                    return;

                hitPoints = value;
                OnPropertyChanged("HitPoints");
            }
        }

        public void AddAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (attachments.ContainsKey(attachment.StateId))
                return;

            attachments.Add(attachment.StateId, attachment);
        }

        public void RemoveAttachment(IAttachableInPlay attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException("attachment");

            if (!attachments.ContainsKey(attachment.StateId))
                return;

            attachments.Remove(attachment.StateId);
        }

        public virtual bool CanPayFor(ICostlyCard costlyCard)
        {
            if (costlyCard == null)
                throw new ArgumentNullException("costlyCard");

            //var cost = costlyCard.GetResourceCost(Game);
            //if (cost == null && Card.PrintedCardType == CardType.Hero)
            //    return true;

            if (Card.HasEffect<IDuringCheckForResourceMatch>())
            {
                var check = new CheckForResourceMatch(Game, costlyCard);

                foreach (var checkEffect in Card.Text.Effects.OfType<IDuringCheckForResourceMatch>())
                {
                    checkEffect.DuringCheckForResourceMatch(check);
                }

                return check.IsResourceMatch;
            }

            return false;
        }

        public virtual bool CanPayFor(ICardEffect cardEffect)
        {
            if (cardEffect == null)
                throw new ArgumentNullException("cardEffect");

            //var costlyEffect = cardEffect as ICostlyEffect;
            //if (costlyEffect != null)
            //{
            //}

            if (Card.HasEffect<IDuringCheckForResourceMatch>())
            {
                var check = new CheckForResourceMatch(Game, cardEffect);

                foreach (var checkEffect in Card.Text.Effects.OfType<IDuringCheckForResourceMatch>())
                {
                    checkEffect.DuringCheckForResourceMatch(check);
                }

                return check.IsResourceMatch;
            }

            return false;
        }

        public override bool HasEffect<TEffect>()
            //where TEffect : IEffect
        {
            if (base.HasEffect<TEffect>())
                return true;

            foreach (var attachment in attachments.Values)
            {
                if (attachment.HasEffect<TEffect>())
                    return true;
            }

            return false;
        }

        public override bool HasTrait(Trait trait)
        {
            if (base.HasTrait(trait))
                return true;

            var check = new CheckForTrait(Game, this, trait);

            foreach (var attachment in attachments.Values.Where(x => x.Card.HasEffect<IDuringCheckForTrait>()))
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
