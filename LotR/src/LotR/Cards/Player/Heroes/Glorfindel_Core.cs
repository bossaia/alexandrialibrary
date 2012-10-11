﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.States;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Glorfindel_Core
        : HeroCardBase
    {
        public Glorfindel_Core()
            : base("Glorfindel", CardSet.Core, 11, Sphere.Lore, 12, 3, 3, 1, 5)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Noldor);
            AddTrait(Trait.Warrior);
        }

        private class PayOneResourceToHealCharacter
            : ActionCharacterAbilityBase
        {
            public PayOneResourceToHealCharacter(Glorfindel_Core source)
                : base("Pay 1 resource from Glordindel's pool to heal 1 damage on any character. (Limit once per round.)", source)
            {
            }

            public override IChoice GetChoice(IPhaseStep step)
            {
                return new ChooseCharacter(Source);
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var resourceful = step.GetCardInPlay(Source.Id) as ICardInPlay<IResourcefulCard>;
                if (resourceful == null || resourceful.Card == null)
                    return null;

                return new PayResourcesFrom(Source, resourceful.Card, 1);
            }

            public override ILimit GetLimit(IPhaseStep step)
            {
                return new Limit(PlayerScope.None, TimeScope.Round, 1);
            }

            public override bool PaymentAccepted(IPhaseStep step, IPayment payment)
            {
                if (payment == null)
                    return false;

                var resourcePayment = payment as IResourcePayment;
                if (resourcePayment == null)
                    return false;

                var firstPayment = resourcePayment.Payments.FirstOrDefault();
                if (firstPayment == null)
                    return false;

                //if (firstPayment.Item1.Id != Source.Id || firstPayment.Item2 != 1)
                //    return false;

                //if (firstPayment.Item1.Resources < 1)
                //    return false;

                //firstPayment.Item1.RemoveResources(1);

                return true;
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var characterChoice = choice as IChooseCharacter;
                if (characterChoice == null || characterChoice.Character == null)
                    return;

                //step.AddStep(new HealCharacterDamageStep(step.Phase, step.Player, characterChoice.Character, 1));
            }
        }
    }
}
