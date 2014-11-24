﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class OverTheMistyMountainsGrimNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Over the Misty Mountains Grim Nightmare";
            Abbreviation = "OtMMGN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";
            Number = 2051;

            Cards.Add(new Card()
            {
                Title = "Over the Misty Mountains Grim Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Png,
                Id = "857AD504-541B-48B0-9DEB-AB31B122C50D",
                CardType = CardType.Nightmare_Setup,
                Text = "You are playing Nightmare mode.\r\n\r\nWhen resolving the \"when revealed\" effect of stage 3B, no more than X resources may be spent from Bilbo Baggins's resource pool, where X is the number of players in the game.",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for the Over the Misty Mountains Grim scenario. This scenario has two encounter decks. The first encounter deck consists of Over the Misty Mountains Grim and Western Lands encounter sets, and the second consists of The Great ~Goblin and Misty ~Mountain Goblins encounter sets.

Remove the following cards, in the specified quantities, from both of the standard encounter deck:

2x ~Cave Entrance
2x Lone-lands
2x Dreary ~Hills
2x Overhanging Rock
1x Great Cavern Room
1x The Great ~Goblin
3x ~Goblin Miners
3x ~Goblin Axeman

Then, shuffle the encounter cards in this Nightmare Deck with the Over the Misty Mountains Grim encounter set icons into the remainder of the first encounter deck. Shuffle the cards with The Great ~Goblin encounter set icon into the second encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effects remain active throughout the game, which is now ready to begin.",
                EncounterSet = "Over the Misty Mountains Grim Nightmare",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Mark_Bulahao,
                UpdateScenarioCards = (groups) =>
                {
                    foreach (var group in groups)
                    {
                        var scenario = group.Scenarios.Where(x => x.Title == "Over the Misty Mountains Grim").FirstOrDefault();
                        if (scenario == null)
                            continue;

                        foreach (var card in scenario.ScenarioCards.Where(x => !x.EncounterSet.EndsWith(" Nightmare")))
                        {
                            switch (card.Title)
                            {
                                case "Cave Entrance":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Lone-lands":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Dreary Hills":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Overhanging Rock":
                                    card.NightmareQuantity -= 2;
                                    break;
                                case "Great Cavern Room":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "The Great Goblin":
                                    card.NightmareQuantity -= 1;
                                    break;
                                case "Goblin Miners":
                                    card.NightmareQuantity -= 3;
                                    break;
                                case "Goblin Axeman":
                                    card.NightmareQuantity -= 3;
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
                Title = "Narrow Ledge",
                ImageType = ImageType.Png,
                Id = "48EE2956-EA04-4F72-A39E-0C561BE576D6",
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 3,
                Traits = new List<string> { "Mountain." },
                Text = "While Narrow Ledge is the active location, each player cannot commit more than 3 characters to the quest.",
                Shadow = "Shadow: Shuffle all copies of Galloping Boulders from the encounter discard pile into the encounter deck.",
                EncounterSet = "Over the Misty Mountains Grim Nightmare",
                Quantity = 3,
                Number = 2,
                Artist = Artist.Niten
            });
            Cards.Add(new Card()
            {
                Title = "Dim Valley",
                ImageType = ImageType.Png,
                Id = "B28CF907-EB44-4A8D-98D7-EDD2DF4623A0",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 10,
                Traits = new List<string> { "Mountain." },
                Text = 
@"Action: Exhaust Bilbo Baggins and spend 1 Baggins resource to place 5 progress on Dim Valley.

Forced: At the end of the round, each player must discard a random card from his hand.",
                FlavorText = "They were high up in a narrow place, with a dreadful fall into a dim vally at one side of them. -The Hobbit",
                EncounterSet = "Over the Misty Mountains Grim Nightmare",
                Quantity = 4,
                Number = 3,
                Artist = Artist.Sabin_Boykinov
            });
            Cards.Add(new Card()
            {
                Title = "Lightning Splinters",
                ImageType = ImageType.Png,
                Id = "739A4125-54A8-4277-8A92-368F54ED3A8F",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must choose and discard 3 cards from his hand. Each player assigns X damage among characters he controls, where X is the combined printed cost of the cards he discarded.",
                FlavorText = "The lighning splinters on the peaks, and rocks shiver, and great crashes split the air and go rolling and tumbling into every cave and hallow... -The Hobbit",
                EncounterSet = "Over the Misty Mountains Grim Nightmare",
                Quantity = 2,
                Number = 4,
                Artist = Artist.JB_Casacop
            });
            Cards.Add(new Card()
            {
                Title = "The Great Goblin",
                ImageType = ImageType.Png,
                Id = "EDA986F6-EB85-4127-9E30-92FC5C373240",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 15,
                Threat = 5,
                Attack = 6,
                Defense = 4,
                HitPoints = 11,
                Traits = new List<string> { "Goblin.", "Orc." },
                Text = "Progress cannot be placed on the current quest.\r\nForced: When The Great Goblin attacks, discard the top card of the encounter deck. If it is a Goblin enemy, put it into play engaged with the defending player and deal it a shadow card.",
                VictoryPoints = 3,
                EncounterSet = "The Great Goblin Nightmare",
                ScenarioTitle = "Over the Misty Mountains Grim Nightmare",
                Quantity = 1,
                Number = 5,
                Artist = Artist.Mark_Bulahao
            });
            Cards.Add(new Card()
            {
                Title = "Goblin Prowler",
                ImageType = ImageType.Png,
                Id = "46F01E94-6C05-42A8-AD36-7FA365740F4D",
                CardType = CardType.Enemy,
                EngagementCost = 45,
                Threat = 4,
                Attack = 4,
                Defense = 3,
                HitPoints = 3,
                Traits = new List<string> { "Goblin.", "Orc." },
                Text = 
@"Goblin Prowler gets -30 engagement cost if The Great Goblin is in the victory display.

Forced: When Goblin Prowler engages you, discard a character you control.",
                FlavorText = "...neither Bilbo, nor the dwarves, nor even Gandalf heard them coming. -The Hobbit",
                EncounterSet = "The Great Goblin Nightmare",
                ScenarioTitle = "Over the Misty Mountains Grim Nightmare",
                Quantity = 4,
                Number = 6,
                Artist = Artist.Mariusz_Gandzel
            });
            Cards.Add(new Card()
            {
                Title = "Dark Passages",
                ImageType = ImageType.Png,
                Id = "D9ED9A91-57FF-4235-BE12-D9E02E5953DA",
                CardType = CardType.Location,
                Threat = 4,
                QuestPoints = 3,
                Traits = new List<string> { "Cave." },
                Text = 
@"When Revealed: Choose a location in the staging area with a title other than Dark Passages and make it the active location. (If there was already an actice location, there are now 2 active locations.)

Travel: Raise each player's threat by X to travel here, where X is the number of Cave locations in the staging area.",
                EncounterSet = "The Great Goblin Nightmare",
                ScenarioTitle = "Over the Misty Mountains Grim Nightmare",
                Quantity = 3,
                Number = 7,
                Artist = Artist.Nicholas_Gregory
            });
            Cards.Add(new Card()
            {
                Title = "Swish, smack! Whip crack!",
                ImageType = ImageType.Png,
                Id = "76436D46-2999-4E00-A33D-712D652D569B",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must assign damage among characters he controls equal to the total number of Goblin enemies engaged with him and in the staging area.",
                Shadow = "Shadow: Attacking enemy cannot take damage this round.",
                EncounterSet = "The Great Goblin Nightmare",
                ScenarioTitle = "Over the Misty Mountains Grim Nightmare",
                Quantity = 2,
                Number = 8,
                Artist = Artist.Adam_Lane
            });
        }
    }
}