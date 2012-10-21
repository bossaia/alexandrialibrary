﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Cards.Encounter.Locations
{
    public class NecromancersPass
        : LocationCardBase
    {
        public NecromancersPass()
            : base("Necromancer's Pass", CardSet.Core, 94, EncounterSet.Dol_Guldur_Orcs, 2, 3, 2, 0)
        {
            AddTrait(Trait.Stronghold);
            AddTrait(Trait.Dol_Guldur);

            AddEffect(new TravelFirstPlayerMustDiscardTwoRandomCards(this));
        }

        private class TravelFirstPlayerMustDiscardTwoRandomCards
            : TravelEffectBase
        {
            public TravelFirstPlayerMustDiscardTwoRandomCards(NecromancersPass source)
                : base("The first player must discard 2 cards from his hand at random to travel here.", source)
            {
            }

            public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
            {
                var firstPlayer = game.Players.Where(x => x.IsFirstPlayer).FirstOrDefault();
                if (firstPlayer == null)
                    return false;

                if (firstPlayer.Hand.Cards.Count() < 2)
                    return false;

                var random1 = firstPlayer.Hand.GetRandomCard();
                var random2 = firstPlayer.Hand.GetRandomCard();

                firstPlayer.DiscardFromHand(new List<IPlayerCard> { random1, random2 });

                return true;
            }
        }
    }
}
