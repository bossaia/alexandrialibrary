﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Sets
{
    public class EscapeFromDolGuldurNightmare : CardSet
    {
        protected override void Initialize()
        {
            Name = "Escape From Dol Guldur Nightmare";
            Abbreviation = "EFDGN";
            SetType = Models.SetType.Nightmare_Expansion;
            Cycle = "NIGHTMARE";

            Cards.Add(new Card()
            {
                Title = "Escape from Dol Guldur Nightmare",
                HasSecondImage = true,
                ImageType = Models.ImageType.Jpg,
                Id = "3E562129-6B4E-4DA9-B7DD-8165BB91931B",
                CardType = CardType.Nightmare_Setup,
                Text = "You are playing Nightmare mode.\r\n\r\nForced: When stage 1B is revealed, the number of random heroes captured is determined by the number of players, as follows: 1-2 players = 1 hero captured, 3 players = 2 heroes captured, 4 players = 3 heroes captured. All captured heroes are turned face down and considered \"the prisoner,\" cannot be used, cannot be damaged, and do not collect resources until they are rescued, (A single player cannot have more than 1 hero captured by this effect.)",
                OppositeText =
@"Begin with the standard quest deck and encounter deck for the Escape from Dol Guldur scenario found in the LOTR LCG core set.

Remove the following cards, in the specified quantities, from the standard encounter deck:

2x Dol Guldur Orcs
2x Enchanted ~Stream
1x Great ~Forest Web
3x Mountains of ~Mirkwood
2x Caught in a Web
1x The Necromancer's Reach
1x Necromancer's Pass
2x Under the ~Shadow
2x Tower ~Gate
1x Iron Shackles

Then, shuffle the encounter cards in this Nightmare Deck into the remainder of the standard Escape from Dol Guldur encounter deck.

Finally, flip this setup card over and place it next to the quest deck. Its effects remain active throughout the game, which is now ready to begin.",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card()
            {
                Title = "Torture Master",
                ImageType = ImageType.Jpg,
                Id = "0C6DB523-94DC-4893-9A96-F2D631D9CB3A",
                CardType = CardType.Enemy,
                EngagementCost = 45,
                Threat = 5,
                Attack = 1,
                Defense = 2,
                HitPoints = 4,
                Traits = new List<string> { "Dol Guldur.", "Orc." },
                Text = "When Revealed: Add 1 resource token to each Dol Guldur location in play.",
                Shadow = "Shadow: Any hero damaged but not defeated by this attack is turned face down, as a \"prisoner.\" Then, if the players are on stage 3, return to stage 2B of the quest.",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 3,
                Number = 2,
            });
            Cards.Add(new Card()
            {
                Title = "Crazed Captive",
                ImageType = ImageType.Jpg,
                Id = "54DD806E-767D-4FAB-964D-A0951475CFB9",
                CardType = CardType.Enemy,
                EngagementCost = 13,
                Threat = 2,
                Attack = 1,
                Defense = 3,
                HitPoints = 1,
                Traits = new List<string> { "Captive." },
                Keywords = new List<string> { "Doomed 1." },
                Text = "Forced: if Crazed Captive is defeated, raise each players threat by 7.",
                Shadow = "If attacking enemy is not defeated this phase, raise defending player's threat by 7 at the end of the phase.",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 2,
                Number = 3,
            });
            Cards.Add(new Card()
            {
                Title = "Spider of Dol Guldur",
                ImageType = ImageType.Jpg,
                Id = "D1EB44E3-8ABA-4024-B858-82C9A4EBF374",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Threat = 3,
                Attack = 3,
                Defense = 3,
                HitPoints = 6,
                Traits = new List<string> { "Creature.", "Spider.", "Dol Guldur." },
                Text = "If there are any unclaimed objectives in play, Spider of ~Dol Guldur gains surge.",
                Shadow = "Shadow: Attacking enemy gets +3 Attack for each unclaimed objective in play.",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 3,
                Number = 4,
            });
            Cards.Add(new Card()
            {
                Title = "Torture Chamber",
                ImageType = ImageType.Jpg,
                Id = "CC2D3C5B-E751-4B2D-8177-A9D8792FA4C0",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 6,
                Traits = new List<string> { "Dol Guldur." },
                Text = "If Torture Chamber has 4 or more resource tokens on it, all \"prisoners\" were killed, and the players have lost the game.\r\n\r\nForced: At the end of each round, place 1 resource token on each Dol Guldur location in play.",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 2,
                Number = 5,
            });
            Cards.Add(new Card()
            {
                Title = "Dungeon Labyrinth",
                ImageType = ImageType.Jpg,
                Id = "4780DEFF-40C9-4DEC-BFD8-83D892C76A91",
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 4,
                Traits = new List<string> { "Dol Guldur." },
                Text = 
@"Dungeon Labyrinth gets +1 Threat and +1 quest point for each resource token on it

Forced: At the end of each round, place 1 resource token on Dungeon Labyrinth for each player in the game.",
                FlavorText = "\"Some here will remember that many years ago I myself dared to pass the doors of the Necromancer in Dol Guldur...\" -Gandalf, The Fellowship of the Ring",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 3,
                Number = 6,
            });
            Cards.Add(new Card()
            {
                Title = "Sentinel of Shadow",
                ImageType = ImageType.Jpg,
                Id = "0323B0F5-7726-4F0C-891E-A9F970E1974F",
                CardType = CardType.Treachery,
                Text = "When Revealed: The staging area gets +X Threat until the end of the phase, where X is twice the number of players in the game. If the players quest unsuccessfully this phase, put the ~Nazgûl of ~Dol Guldur into play (from any out of play area), engaged with the first player. (Cannot be canceled.)",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 2,
                Number = 7,
            });
            Cards.Add(new Card()
            {
                Title = "Catacomb Inspection",
                ImageType = ImageType.Jpg,
                Id = "A8750BE5-98C8-4D6C-ACBE-28A9E9B85B3A",
                CardType = CardType.Treachery,
                Keywords = new List<string> { "Surge." },
                Text = "When Revealed: Add 1 resource token to each Dol Guldur location in play.",
                Shadow = "Shadow: If there is a Dol Guldur location in the staging area, move 1 resource token from each of your heroes' resource pools to that location. (Resolve this effect for each Dol Guldur location.)",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 2,
                Number = 8,
            });
            Cards.Add(new Card()
            {
                Title = "Dark Interrogation",
                ImageType = ImageType.Jpg,
                Id = "2DC13AE9-73DD-4F80-AF8F-29E4E4BA7E7A",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must choose and discard cards from hand until he has only 2 cards in hand. (Each player with 2 or fewer cards in hand must instead reveal 1 card from the encounter deck.)",
                Shadow = "Shadow: Discard each card you control that has at least 1 copy of itself in your discard pile. (If this attack is undefended, each player must resolve this effect.)",
                EncounterSet = "Escape from Dol Guldur Nightmare",
                Quantity = 2,
                Number = 9,
            });
        }
    }
}