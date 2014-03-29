using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheStoneofErech : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Stone of Erech";
            Abbreviation = "TSoE";
            Number = 2003;
            SetType = Models.SetType.GenCon_Expansion;
            Cycle = "GenCon";

            Cards.Add(new Card() {
                ImageName = "M1928",
                Title = "Banks of Morthond",
                Id = "14f7c98c-b425-4fac-850a-729d8fcdaa0c",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale.", " Riverland." },
                Text = "While Banks of Morthond is the active location, each location in the staging area gets -1 Threat.",
                Shadow = "Shadow: If this attack destroys a character, add 1 progress to the current Night objective.",
                Quantity = 2,
                Threat = 2,
                QuestPoints = 7,
                Number = 17
            });
            Cards.Add(new Card() {
                ImageName = "M1930",
                Title = "Blackroot Graves",
                Id = "fc54b033-56cc-4ca9-8e0f-7f1c7b82bd6a",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Text = "Forced: When Blackroot Graves is explored, return the topmost Undead enemy in the encounter discard pile to play, engaged with the first player.",
                Shadow = "Shadow: If defending character has 0 Willpower, deal 2 damage to that character.",
                Quantity = 3,
                Threat = 4,
                QuestPoints = 1,
                Number = 19
            });
            Cards.Add(new Card() {
                ImageName = "M1915",
                Title = "Derufin",
                Id = "f93a8fda-7383-41c9-b86d-c5ba1fec760d",
                CardType = CardType.Objective_Ally,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Gondor." },
                Text = 
@"Forced: After the players travel to The Stone of Erech, the first player gains control of Derufin.

Dusk. Derufin gets -1 Willpower

Midnight. Derufin gets -2 Willpower

If Derufin leaves play, the players lose the game.",
                Quantity = 1,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 3,
                HitPoints = 2,
                Number = 4
            });
            Cards.Add(new Card() {
                ImageName = "M1936",
                Title = "Driven by Fear",
                Id = "cdf8dee9-b2ac-4e5f-bf4c-0b9c15cac142",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Text = "When Revealed: The first player attached Driven by Fear to a hero he controls. Counts as a Condition attachment with the text: \"Attached character's Willpower is reduced to 0. Treat attacked character's text box as if it was blank (except for Traits.)\"",
                Quantity = 2,
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1920",
                Title = "Dusk",
                Id = "d3c68791-c3f9-4e8d-9515-2496f9ca1895",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Night." },
                Text =
@"Forced: At the end of the round, place 1 progress on Dusk.

If there are 4 or more progress on Dusk, add it to the victory display and put Midnight into play.",
                Quantity = 1,
                Number = 9
            });
            Cards.Add(new Card() {
                ImageName = "M1919",
                Title = "Eventide",
                Id = "36a22f2b-56d9-4148-a962-f7fbdf60f3a1",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Night." },
                Text =
@"Forced: At the end of the round, place 1 progress on Eventide.

If there are 4 or more progress on Eventide, add it to the victory display and put Dusk into play.",
                Quantity = 1,
                Number = 8
            });
            Cards.Add(new Card() {
                ImageName = "M1935",
                Title = "Groping Horror",
                Id = "728f17db-d741-4f1b-92da-8aac3d78432d",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Text =
@"When Revealed: The first player must choose: skip the next travel phase, or each player must pass his hand to the player on his left (discard your hand instead if you are the only player in the game).

Midnight. Groping Horror gains surge.",
                Quantity = 2,
                Number = 24
            });
            Cards.Add(new Card() {
                ImageName = "M1932",
                Title = "Haunted Valley",
                Id = "7a3411df-76a0-432b-a64a-d499b0b1ae50",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Text =
@"While this location is in the staging area, all ready heroes lose all Lore, Leadership, Spirit and Tactics icons.

Planning Action: Exhuast a hero. Any player may trigger this action.",
                Quantity = 2,
                Threat = 2,
                QuestPoints = 3,
                Number = 21
            });
            Cards.Add(new Card() {
                ImageName = "M1921",
                Title = "Midnight",
                Id = "0c15f99d-9066-4e7c-b364-104a0083d997",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Night." },
                Text = "Forced: At the end of the round, place 1 progress on Midnight. Then, raise each player's threat by the number of progress on Midnight.",
                Quantity = 1,
                Number = 10
            });
            Cards.Add(new Card() {
                ImageName = "M1933",
                Title = "Midnight Throng",
                Id = "c29c98de-5660-498c-a5cc-a1800985d221",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Text =
@"When Revealed: Starting with the first player, each player must search the encounter deck and discard pile for an Undead enemy, reveal it, and add it to the staging area. Shuffle the encounter deck.

Midnight. THe effect cannot be canceled.",
                Shadow = "Attacking enemy gets +1 Attack (Dusk. +2 Attack instead. Midnight. +3 Attack instead).",
                Quantity = 2,
                Number = 22
            });
            Cards.Add(new Card() {
                ImageName = "M1934",
                Title = "Murmurs of Dread",
                Id = "f985bda2-5029-4c34-91f7-721f830cbd9c",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Text = "When Revealed: All characters get -1 Willpower until the end of the round.",
                Shadow = 
@"Dusk/Midnight. Attack enemy makes an additional attack after this one.
                
Midnight. This effect cannot be canceled.",
                Quantity = 2,
                Number = 23
            });
            Cards.Add(new Card() {
                ImageName = "M1927",
                Title = "Regretful Shade",
                Id = "f079c496-20bf-4ce4-a5ca-d644172d83fe",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 3,
                EngagementCost = 23,
                Attack = 2,
                Defense = 1,
                HitPoints = 3,
                Keywords = new List<string>() { "Spectral.", " Surge." },
                Shadow = "Deal Attacking enemy 1 additional shadow card (Dusk. 2 additional cards instead. Midnight. 3 additional cards instead).",
                Threat = 1,
                Number = 16
            });
            Cards.Add(new Card() {
                ImageName = "M1922",
                Title = "Relic from the Dark Years",
                Id = "7b0eb4e5-962c-4041-baea-3a09a83fb996",
                CardType = CardType.Objective,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Item.", " Artifact." },
                Text = "Midnight. Action: Claim this objective and attach it to a hero you control. Counts as an Artifact attachment with the text: \"When attached hero attacks and Oathbreaker, that enemy loses the Spectral keyword until the end of the phase.\"",
                Quantity = 1,
                Number = 11
            });
            Cards.Add(new Card() {
                ImageName = "M1924",
                Title = "Restless Dead",
                Id = "65f48337-cec0-41a1-84ff-490c4f563ce4",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 4,
                EngagementCost = 12,
                Attack = 3,
                Defense = 0,
                HitPoints = 2,
                Keywords = new List<string>() { "Spectral." },
                Text = "When Revealed: Return the topmost Undead enemy in the encounter discard pile to the staging area.",
                Threat = 2,
                Number = 13
            });
            Cards.Add(new Card() {
                ImageName = "M1926",
                Title = "Shadow Host Captain",
                Id = "a7d52939-2989-44d2-b81b-571db0ee8c16",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 2,
                EngagementCost = 42,
                Attack = 4,
                Defense = 1,
                HitPoints = 6,
                Keywords = new List<string>() { "Spectral." },
                Text =
@"Dusk. Shadow Host Captain gets +1 Attack and +1 Defense.

Midnight. Shadow Host Captain gets +2 Attack and +2 Defense.",
                Threat = 3,
                Number = 15
            });
            Cards.Add(new Card() {
                ImageName = "M1923",
                Title = "Shadow-man",
                Id = "8ece887c-6ca7-467d-80d4-3f28afa13433",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 2,
                EngagementCost = 27,
                Attack = 4,
                Defense = 1,
                HitPoints = 5,
                Keywords = new List<string>() { "Spectral." },
                Text = "When Revealed: Players cannot play events until the end of the round.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack (Dusk. +2 Attack instead. Midnight. +3 Attack instead).",
                Threat = 2,
                Number = 12
            });
            Cards.Add(new Card() {
                ImageName = "M1931",
                Title = "Shadow of Dwimorberg",
                Id = "02cae27a-3b0c-4396-aea3-8fc2bdd4ffe8",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale.", " Mountain." },
                Text =
@"Dusk. Shadow of Dwimorberg gets +2 Threat.


Midnight. Shadow of Dwimorberg gets +4 Threat.",
                Quantity = 2,
                Threat = 1,
                QuestPoints = 4,
                Number = 20
            });
            Cards.Add(new Card() {
                ImageName = "M1918",
                Title = "Tarlang's Neck",
                Id = "88df7c56-ee11-48f0-b6a0-0486e0a8ce92",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale.", " Road." },
                Text = "While Tarlang's Next is the active location, it gains, \"Forced: At the beginning of the encounter phase, the players must either immediately end the encounter phase, or the first player must engaged all enemies in the staging area.\"",
                Quantity = 1,
                IsUnique = true,
                Threat = 3,
                QuestPoints = 6,
                VictoryPoints = 1,
                Number = 7
            });
            Cards.Add(new Card() {
                ImageName = "M1913",
                Title = "Terror of the Dead",
                StageNumber = 2,
                Id = "ecc8fd11-b2b9-4adc-91eb-6fcc1212e7ce",
                CardType = CardType.Quest,
                EncounterSet = "The Stone of Erech",
                Text = "When Revealed: Add The Stone of Erech to the staging area and attach Derufin to it.",
                OppositeText = "Forced: At the beginning of each round, the players must choose: each player skips the next planning phase, or heroes do not collect resources during the resource phase this round.",
                Quantity = 1,
                QuestPoints = 8,
                Number = 2
            });
            Cards.Add(new Card() {
                ImageName = "M1937",
                Title = "The Dead Ride Behind",
                Id = "5e7c36de-a2bb-44e3-951b-80123788d571",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Text = "When Revealed: Deal 1 damage to each character with less than 2 Willpower. (Dusk. less than 3 Willpower insread. Midnight. less than 4 Willpower instead).",
                Quantity = 2,
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1912",
                Title = "The Disappearance",
                StageNumber = 1,
                Id = "e4af5d7f-9af5-41bb-b22a-cce8b91ac791",
                CardType = CardType.Quest,
                EncounterSet = "The Stone of Erech",
                Quantity = 1,
                QuestPoints = 6,
                Text = "Setup: Make Tarlang's Next the active location. Set Derufin, The Lord of the Dead, The Stone of Erech, and the 3 Night objectives aside, out of play. Shuffle the encounter deck. Place Eventide into play, next to the current quest.",
                OppositeText = 
@"When Revealed: Reveal 1 encounter card per player, adding them to the staging area.

Players cannot defeat this stage while Tarlang's Next is in play.",
                Number = 1
            });
            Cards.Add(new Card() {
                ImageName = "M1938",
                Title = "The Gloaming",
                Id = "d33ef89b-13c9-49b2-a921-a527f27a415a",
                CardType = CardType.Treachery,
                EncounterSet = "The Stone of Erech",
                Quantity = 2,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Add 1 progress to the current Night objective.",
                Number = 27
            });
            Cards.Add(new Card() {
                ImageName = "M1916",
                Title = "The Lord of the Dead",
                Id = "7b9dd852-ed19-4a00-b09e-f6c2cee549fa",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 2,
                EngagementCost = 30,
                IsUnique = true,
                Attack = 6,
                Defense = 3,
                HitPoints = 9,
                Keywords = new List<string>() { "Spectral.", " Cannot have attachments." },
                Text = "While you are engaged with The Lord of the Dead, treat all printed text boxes on characters you control as if they were blank (except Traits.)",
                Threat = 5,
                Number = 5
            });
            Cards.Add(new Card() {
                ImageName = "M1914",
                Title = "The Shadow Host",
                StageNumber = 3,
                Id = "efd4cef9-ce8b-4985-8825-853ba98baa2b",
                CardType = CardType.Quest,
                EncounterSet = "The Stone of Erech",
                Text =
@"When Revealed: Starting with the first player, each player searches the encounter deck and discard pile for 1 Oathbreaker enemy, reveals it, and adds it to the staging area. Add the Lord of the Dead to the staging area.

Shuffle the encounter deck.",
                OppositeText = 
@"Midnight. Battle (Characters use their Attack instead of Willpower when questing.)

Players cannot place progress on this stage unless they control Derufin.

If the players defeat this stage, they have escaped the Blackroot Vale with Derufin and won the game.",
                Quantity = 1,
                QuestPoints = 14,
                Number = 3
            });
            Cards.Add(new Card() {
                ImageName = "M1917",
                Title = "The Stone of Erech",
                Id = "822ca699-4763-4b8c-baff-d8d828c002a9",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Quantity = 1,
                IsUnique = true,
                Keywords = new List<string>() { "Immune to player card effects.", },
                Text =
@"X is twice the number of players in the game.

While The Stone of Erech is that active location, characters with less than 2 Willpower cannot ready.

Travel: Exhaust each character with less than 2 Willpower.",
                Threat = 0,
                IsVariableThreat = true,
                QuestPoints = 6,
                VictoryPoints = 5,
                Number = 6
            });
            Cards.Add(new Card() {
                ImageName = "M1929",
                Title = "Vale of Shadows",
                Id = "c57661b7-62f9-4906-b2eb-73af91fd8252",
                CardType = CardType.Location,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Blackroot Vale." },
                Text =
@"When Revealed: Make Vale of Shadows the active location. If another location is currently active, return it to the staging area.

Dusk. Vale of Shadows gets +2 quest points.

Midnight. Vale of Shadows gets +4 quest points.",
                Quantity = 2,
                Threat = 1,
                QuestPoints = 3,
                Number = 18
            });
            Cards.Add(new Card() {
                ImageName = "M1925",
                Title = "Whisperer",
                Id = "43bb02ff-9fcc-47e8-822c-ffcd620c9fc4",
                CardType = CardType.Enemy,
                EncounterSet = "The Stone of Erech",
                Traits = new List<string>() { "Undead.", " Oathbreaker." },
                Quantity = 3,
                EngagementCost = 35,
                Attack = 2,
                Defense = 2,
                HitPoints = 4,
                Keywords = new List<string>() { "Spectral." },
                Text = 
@"Dusk. Whisperer gets +1 Threat.

Midnight. Whisperer gets +2 Threat and gains Doomed 2.",
                Threat = 2,
                Number = 14
            });
        }
    }
}
