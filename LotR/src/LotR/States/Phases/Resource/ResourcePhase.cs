﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Resource;
using LotR.States.Controllers;
using LotR.States.Phases.Any;

namespace LotR.States.Phases.Resource
{
    public class ResourcePhase
        : PhaseBase, IResourcePhase
    {
        #region Structs

        private struct CollectingResourceInfo
        {
            public CollectingResourceInfo(CollectingResourcesEffect effect, CollectingResources state)
            {
                this.effect = effect;
                this.state = state;
            }

            private readonly CollectingResourcesEffect effect;
            private readonly CollectingResources state;

            public CollectingResourcesEffect Effect
            {
                get { return effect; }
            }

            public CollectingResources State
            {
                get { return state; }
            }
        }

        #endregion

        public ResourcePhase(IGame game)
            : base(game, PhaseCode.Resource, PhaseStep.Resource_Start)
        {
        }

        private void EachPlayerCollectsResources()
        {
            var infos = new List<CollectingResourceInfo>();

            foreach (var player in Game.Players)
            {
                foreach (var resourceful in player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card is IResourcefulCard))
                {
                    var state = new CollectingResources(Game, resourceful, 1);
                    var effect = new CollectingResourcesEffect(Game, state);
                    infos.Add(new CollectingResourceInfo(effect, state));
                }
            }

            foreach (var player in Game.Players)
            {
                foreach (var card in player.CardsInPlay.Where(x => x.HasEffect<IDuringCollectingResources>()))
                {
                    foreach (var duringCollectingResourcesEffect in card.BaseCard.Text.Effects.OfType<IDuringCollectingResources>())
                    {
                        foreach (var info in infos)
                        {
                            duringCollectingResourcesEffect.DuringCollectingResource(info.State);
                        }
                    }
                }
            }

            foreach (var info in infos)
            {
                var handle = info.Effect.GetHandle(Game);

                Game.AddEffect(info.Effect);
                Game.TriggerEffect(handle);
            }
        }

        private void EachPlayerDrawsCards()
        {
            var playerDrawOptions = new Dictionary<Guid, Tuple<bool, uint>>();

            foreach (var player in Game.Players)
            {
                playerDrawOptions[player.StateId] = new Tuple<bool, uint>(true, 1);
            }

            var playersDrawingCards = new PlayersDrawingCards(Game, playerDrawOptions);

            foreach (var card in Game.GetCardsInPlayWithEffect<ICardInPlay, IDuringDrawingResourceCards>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IDuringDrawingResourceCards>())
                {
                    effect.DuringDrawingResourceCards(playersDrawingCards);
                }
            }

            foreach (var player in Game.Players)
            {
                if (!playersDrawingCards.Players.Contains(player.StateId))
                    continue;

                uint numberOfCards = playersDrawingCards.PlayerCanDrawCards(player.StateId) ?
                    playersDrawingCards.GetNumberOfCards(player.StateId)
                    : 0;

                if (numberOfCards > 0)
                {
                    var drawCardsEffect = new DrawingCardsEffect(Game, player, numberOfCards);
                    var drawCardsHandle = drawCardsEffect.GetHandle(Game);
                    Game.TriggerEffect(drawCardsHandle);

                    //var gandalf = player.Deck.Cards.Where(x => x.Title == "Gandalf").FirstOrDefault();
                    //if (gandalf != null)
                    //{
                    //    player.Deck.RemoveFromDeck(gandalf);
                    //    player.Hand.AddCards(new List<IPlayerCard> { gandalf });
                    //}

                    //var sneakAttack = player.Deck.Cards.Where(x => x.Title == "Sneak Attack").FirstOrDefault();
                    //if (sneakAttack != null)
                    //{
                    //    player.Deck.RemoveFromDeck(sneakAttack);
                    //    player.Hand.AddCards(new List<IPlayerCard> { sneakAttack });
                    //}
                }
            }
        }

        public override void Run()
        {
            StepCode = PhaseStep.Resource_Add_Resources;

            EachPlayerCollectsResources();

            StepCode = PhaseStep.Resource_Draw_Cards;

            EachPlayerDrawsCards();

            StepCode = PhaseStep.Resource_Player_Actions_Before_End;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Resource_End;
        }
    }
}
