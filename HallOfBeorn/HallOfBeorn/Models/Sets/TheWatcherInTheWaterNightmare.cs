﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class TheWatcherInTheWaterNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Watcher in the Water Nightmare";
            Abbreviation = "TWitWN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2055;

            Cards.Add(new Card()
            {
                Title = "The Watcher in the Water Nightmare",
                Id = "F0B86657-B29E-4428-BD3D-7111E78A8B3A",
                CardType = Models.CardType.Nightmare_Setup,
                Text =
@"You are playing in Nightmare mode.

While Doors of Durin is in play, The Watcher cannot be engaged.

Forced: When a Tentacle enemy is destroyed, deal 1 damage to The Watcher if it is in play.

The players cannot win the game unless both Doors of Durin and The Watcher are in the victory display.",
                FlavorText = "\"Something has crept, or has been driven out of dark waters under the mountains. There are older and fouler things than Orcs in the deep places of the world.\"\r\n-Gandalf, Lord of the Rings",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for The Watcher in the Water scenario.

Remove the following cards, in the specified quantities, from the standard encounter deck:

1x The Watcher
2x Makeshift Passage
4x Black Uruks
3x Mountain Warg
2x Warg Lair

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard The Watcher in the Water encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effect remains active throughout the game, which is now ready to begin.",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Alvaro_Calvo_Escudero,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "The Watcher in the Water").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "The Watcher":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Makeshift Passage":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Black Uruks":
                                    card.NightmareQuantity -= 4;
                                    break;
                                case "Mountain Warg":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Warg Lair":
                                    card.NightmareQuantity -= 2;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    return true;
                }
            });
            Cards.Add(new Card()
            {
                Title = "The Watcher",
                Id = "38396B24-B249-4A91-A458-1008FF0F0B48",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Enemy,
                EngagementCost = 50,
                Threat = 4,
                Attack = 7,
                Defense = 7,
                HitPoints = 21,
                Traits = new List<string> { "Creature.", "Tentacle." },
                Text = 
@"While THe Watcher is in the staging area, it cannot be damage by player card effects.

If The Watcher is in the staging area at the end of the combat phase, each player must deal 3 damage to 1 character he controls.",
                VictoryPoints = 3,
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 1,
                Number = 2,
                Artist = Artist.Lukasz_Jaskolski
            });
            Cards.Add(new Card()
            {
                Title = "Writhing Tentacle",
                Id = "08482A88-5C28-42A4-B21A-C603E2835F75",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Enemy,
                EngagementCost = 46,
                Threat = 4,
                Attack = 4,
                Defense = 0,
                HitPoints = 3,
                Traits = new List<string> { "Tentacle." },
                Text = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 3,
                Artist = Artist.Lukasz_Jaskolski
            });
            Cards.Add(new Card()
            {
                Title = "Choking Tentacle",
                Id = "4CF9E883-D264-495A-B57A-15B8B8995505",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Enemy,
                EngagementCost = 10,
                Threat = 2,
                Attack = 5,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string> { "Tentacle." },
                Text = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 4,
                Number = 4,
                Artist = Artist.Jon_Bosco
            });
            Cards.Add(new Card()
            {
                Title = "Banks of Sirannon",
                Id = "149AC0A6-D778-4A5E-9F6C-CE9CF91CE15F",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string> { "Mountain.", "Swamp." },
                Text = "",
                Shadow = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 5,
                Artist = Artist.Rick_Price
            });
            Cards.Add(new Card()
            {
                Title = "Hideous Depths",
                Id = "ED5D88D2-9559-4CF5-8BA5-562B383062F1",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 5,
                Traits = new List<string> { "Swamp." },
                Text = "",
                Shadow = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 6,
                Artist = Artist.Mark_Behm
            });
            Cards.Add(new Card()
            {
                Title = "Pulled Under",
                Id = "B4E0019A-44E3-411C-8C8C-2812C62C41D8",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Treachery,
                Traits = new List<string> { "Tentacle." },
                Text = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 2,
                Number = 7,
                Artist = Artist.Guillaume_Ducos
            });
            Cards.Add(new Card()
            {
                Title = "Rippling From Beneath",
                Id = "BF528396-B36D-4326-B626-1DE41267EA8B",
                ImageType = Models.ImageType.Png,
                CardType = CardType.Treachery,
                Traits = new List<string> { "Tentacle." },
                Keywords = new List<string> { "Surge." },
                Text = "",
                FlavorText = "",
                EncounterSet = "The Watcher in the Water Nightmare",
                Quantity = 3,
                Number = 8,
                Artist = Artist.Alvaro_Calvo_Escudero
            });
        }
    }
}