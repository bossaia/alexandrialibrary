using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheRedhornGate : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Redhorn Gate";
            Abbreviation = "TRG";
            Number = 9;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Dwarrowdelf";

            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Arwen Und�miel",
                NormalizedTitle = "Arwen Undomiel",
                Id = "231bf335-bf9b-44d9-a919-e59aa1d46473",
                CardType = CardType.Objective_Ally,
                IsUnique = true,
                Attack = 0,
                Defense = 1,
                Willpower = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Noldor.", " Noble.", " Ally." },
                Text = "The first player gains control of Arwen Undomiel, as an ally.Response: After Arwen Undomiel exhausts, choose a hero. Add 1 resource to that hero's resource pool.If Arwen Undomiel leaves play, the players are defeated.",
                EncounterSet = "The Redhorn Gate",
                Quantity = 1,
                Number = 14,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Avalanche!",
                Id = "ef0a0467-656b-4cef-baf2-e60037d69472",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Snow." },
                Text = "When Revealed: Exhaust each ready character and if it is the quest phase commit them to the quest.",
                EncounterSet = "The Redhorn Gate",
                Quantity = 1,
                EasyModeQuantity = 0,
                Number = 24,
                Artist = Artist.David_Lecossu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Bofur",
                Id = "c00bdeff-c186-4d83-bf24-2142fdd39b19",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Dwarf." },
                Text = "Quest Action: Spend 1 Spirit resource to put Bofur into play from your hand, exhausted and committed to a quest. If you quest successfully this phase and Bofur is still in play, return him to your hand.",
                Quantity = 3,
                Number = 6,
                Artist = Artist.Ilich_Henriquez
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Caradhras",
                Id = "f433a8a8-00ee-45d3-b0ac-1643216a2422",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Mountain.", " Snow." },
                Text = "While Caradhras is the active location, questing characters get -1 Willpower.Players cannot travel to Caradhras except by quest card effects.",
                Threat = 3,
                QuestPoints = 9,
                EncounterSet = "The Redhorn Gate",
                Quantity = 1,
                VictoryPoints = 3,
                Number = 15,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Celebdil",
                Id = "1ce63cb5-0022-48b2-a864-af49bdf18bef",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Mountain.", " Snow." },
                Text = "While Celebdil is the active location, remove 2 progress tokens from it at the end of each round.",
                Threat = 3,
                QuestPoints = 7,
                EncounterSet = "The Redhorn Gate",
                Quantity = 1,
                VictoryPoints = 2,
                Number = 17,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Elrohir",
                Id = "cc7beee8-1f42-4926-8c45-8a50f3a87c57",
                CardType = CardType.Hero,
                Sphere = Sphere.Leadership,
                ThreatCost = 10,
                IsUnique = true,
                Attack = 2,
                Defense = 1,
                Willpower = 2,
                HitPoints = 4,
                Traits = new List<string>() { "Noldor.", " Noble.", " Ranger." },
                Text = "While Elladan is in play, Elrohir gets +2 Defense.Response: After Elrohir is declared as a defender, pay 1 resource from his resource pool to ready him.",
                Quantity = 1,
                Number = 1,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Fallen Stones",
                Id = "0a1166fb-fb90-4651-a8ae-7d532e41c2b6",
                CardType = CardType.Treachery,
                Text = "When Revealed: The first player (choose 1): removes all progress tokens from play, or reveals 2 cards from the encounter deck and adds them to the staging area.",
                Shadow = "Shadow: attacking enemy gets +1 Attack for each progress token on the active location.",
                EncounterSet = "The Redhorn Gate",
                Quantity = 2,
                Number = 21,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Fanuidhol",
                Id = "71f1436f-bc66-4bf9-ac87-369ddea7dc64",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Mountain.", " Snow." },
                Text = "While Fanuidhol is the active loction, heroes must spend 1 resource from their resource pool to count their Willpower during the quest phase.",
                Threat = 3,
                QuestPoints = 7,
                EncounterSet = "The Redhorn Gate",
                Quantity = 1,
                VictoryPoints = 2,
                Number = 16,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Fell Voices",
                Id = "14be3661-3f3c-4a89-b27f-ab7c15f92e4e",
                CardType = CardType.Treachery,
                Text = "When Revealed: Return the top 2 Snow cards in the encounter discard pile to the top of the encounter deck. If this effect returned less than 2 Snow treachery cards, Fell Voices gains surge.",
                EncounterSet = "The Redhorn Gate",
                Quantity = 2,
                Number = 20,
                Artist = Artist.K_R_Harris
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Freezing Cold",
                Id = "2e810a9b-5d79-4828-8268-33e82c5fc1fa",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Snow." },
                Text = "When Revealed: The first player attaches this card to a hero he controls. Counts as a Condition Attachment with the text: 'Attached hero gets -2 Willpower and cannot commit to a quest. If attached hero has more than 1 copy of Freezing Cold attached, discard attached hero from play.'",
                EncounterSet = "The Redhorn Gate",
                Quantity = 3,
                EasyModeQuantity = 1,
                Number = 23,
                Artist = Artist.Eric_Braddock
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Good Meal",
                Id = "97016882-11f8-428c-9f3f-43bb51aacb38",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                ResourceCost = 0,
                Text = "Action: Discard Good Meal to lower the cost of the next event you play this round that matches attached hero's sphere by 2.",
                Keywords = new List<string>() { "Attach to a Hobbit hero." },
                Quantity = 3,
                Number = 10,
                Artist = Artist.David_A_Nash
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Keeping Count",
                Id = "fcfc6d7e-40eb-43b4-a72a-b529931f12ea",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Text = 
@"Attached hero gets +1 Attack for each resource token on another copy of Keeping Count that is above the current number of resource tokens on this card.

Forced: After attached hero attacks and destroys an enemy, place 1 resource token on this card.",
                Keywords = new List<string>() { "Attach to a hero.", " Limit 1 per hero." },
                Quantity = 3,
                Number = 5,
                Artist = Artist.Mark_Tarrisse
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Mountain Goblin",
                Id = "2809f147-b000-419c-bbb2-39e6221d72de",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Goblin.", " Orc." },
                Text = "~Mountain Goblin gets +1 Attack for each Mountain location in the staging area.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if the active location is a Mountain.)",
                Threat = 1,
                EncounterSet = "The Redhorn Gate",
                Quantity = 3,
                Number = 25,
                Artist = Artist.Dmitry_Burmak
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Mountain Troll",
                Id = "ca41e49b-6bf0-42dc-8f83-83d7c8aaba1c",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Attack = 5,
                Defense = 5,
                HitPoints = 7,
                Traits = new List<string>() { "Troll." },
                Text = "~Mountain ~Troll gets +1 Attack for each Mountain location in the staging area.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+2 Attack instead if the active location is a Mountain.)",
                Threat = 2,
                EncounterSet = "The Redhorn Gate",
                Quantity = 2,
                EasyModeQuantity = 0,
                Number = 26,
                Artist = Artist.Rafal_Hrynkiewicz
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Needful to Know",
                Id = "28a27a2b-d9c9-40ab-95ec-b44eca301102",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                ResourceCost = 2,
                Text = "Action: Raise your threat by 1 to look at the top card of the ecnounter deck. Then, reduce your threat by X, where X is the threat of that card.",
                Keywords = new List<string>() { "Secrecy 2." },
                Quantity = 3,
                Number = 9,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Ravenhill Scout",
                Id = "2bb2c224-3d72-455f-9ec6-89670d372660",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 0,
                HitPoints = 3,
                Traits = new List<string>() { "Dale.", " Scout." },
                Text = "Action: Exhaust Ravenhill Scout to move up to 2 progress tokens from 1 location to another location.",
                FlavorText = "\"They made their first camp on the western side of the great southern spur, which ended in a height called Ravenhill.\" -The Hobbit",
                Quantity = 3,
                Number = 8,
                Artist = Artist.Garret_DeChellis
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Renewed Friendship",
                Id = "ef533832-523e-40ff-9116-163485826c5b",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 0,
                Text = "Response: After another player plays an attachment on a hero you control, you may (choose 1): ready 1 of that player's heroes, have that player draw 1 card, or lower that player's threat by 2.",
                Quantity = 3,
                Number = 7,
                Artist = Artist.Sara_Biddle
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Rocky Crags",
                Id = "f8edfe48-aeda-41ea-bef9-db49d4781e7b",
                CardType = CardType.Location,
                Traits = new List<string>() { "Mountain." },
                Text = "Travel: Each player must deal 2 damage to 1 character he controls to travel here.",
                Shadow = "Shadow: attacking enemy gets +1 Attack for each progress token on the active location.",
                Threat = 4,
                QuestPoints = 2,
                EncounterSet = "The Redhorn Gate",
                Quantity = 3,
                EasyModeQuantity = 1,
                Number = 19,
                Artist = Artist.David_Lecossu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Snowstorm",
                Id = "e6808a55-528f-444e-bf61-67748e7261e9",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Snow." },
                Text = "When Revealed: Each questing character gets -1 Willpower until the end of the phase.",
                Shadow = "Shadow: Until the end of the phase, characters defending this attack get -1 Willpower and are discarded if their Willpower is 0.",
                EncounterSet = "The Redhorn Gate",
                Quantity = 5,
                Number = 22,
                Artist = Artist.David_Lecossu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Snow Warg",
                Id = "a516b400-b53f-4c21-8e44-c0a889caa8d5",
                CardType = CardType.Enemy,
                EngagementCost = 28,
                Attack = 3,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Creature.", " Snow." },
                Text = 
@"Allies cannot defend while Snow Warg is attacking.

Forced: After a character is declared as a defender against Snow Warg, deal 1 damage to the defending character, if able.",
                Threat = 3,
                EncounterSet = "The Redhorn Gate",
                Quantity = 3,
                Number = 27,
                Artist = Artist.Allison_Theus
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Taking Initiative",
                Id = "ac2d3796-1d68-4d27-9e5e-4a09a535c0b7",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 0,
                Text = "Action: Discard the top card of your deck. If the discarded card's printed cost is equal to or higher than the number of characters you control, draw 2 cards and deal 2 damage to any enemy.",
                Quantity = 3,
                Number = 2,
                Artist = Artist.Melissa_Findley
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Dimrill Stair",
                Id = "a4df63d1-cb3d-4510-9e26-3b734632f456",
                CardType = CardType.Location,
                IsUnique = true,
                Traits = new List<string>() { "Stair." },
                Text = "Travel: Reshuffle all locations in the discard pile and victory display back into the encounter deck. If you reshuffled at least two locations, reduce each player's threat by 11 and discard all copies of Freezing Cold from play.",
                Threat = 2,
                QuestPoints = 3,
                EncounterSet = "The Redhorn Gate",
                Quantity = 1,
                VictoryPoints = 1,
                Number = 18,
                Artist = Artist.Trudi_Castle
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Timely Aid",
                Id = "bd20ff6a-d77f-47d8-81dd-202e5f21cfaf",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                Text = "Action: Reveal the top 5 cards of your deck and put 1 revealed ally into play, if able. Shuffle all other revealed cards back into your deck.",
                Keywords = new List<string>() { "Secrecy 3." },
                Quantity = 3,
                Number = 3,
                Artist = Artist.Sandara_Tang
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Unseen Strike",
                Id = "c1c21875-2650-4e7d-b8c7-c0c0970ce8b0",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Text = "Action: Choose a character you control. Until the end of the phase, that character gets +3 Attack while attacking an enemy with a higher engagement cost than your threat.",
                Quantity = 3,
                Number = 4,
                Artist = Artist.Even_Mehl_Amundsen
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Snowdrifts",
                StageNumber = 2,
                Id = "737a8d5f-2f82-47e4-844e-52fdae95ccf5",
                CardType = CardType.Quest,
                EncounterSet = "The Redhorn Gate",
                OppositeText = "When Revealed: Shuffle 1 more copy of Snowstorm into the encounter deck than the number of players in the game.\r\nForced: After playing the 11th progress token on Snowdrifts, discard any active location. Caradhras becomes the active location.",
                QuestPoints = 11,
                Quantity = 1,
                Number = 12,
                Artist = Artist.Stu_Barnes,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.MistyMountains }
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Mountains' Peaks",
                StageNumber = 3,
                Id = "001dbc8b-3b8e-4f02-bdbd-7e78c6ecd348",
                CardType = CardType.Quest,
                EncounterSet = "The Redhorn Gate",
                OppositeText = 
@"When Revealed: Shuffle all copies of Snowstorm in the encounter discard pile back into the encounter deck.

Characters are discarded from play if their Willpower is ever 0.

Players cannot defeat this stage unless they have 5 victory points. If the players defeat this stage, they have won the game.",
                Quantity = 1,
                QuestPoints = 13,
                Number = 13,
                Artist = Artist.Cristi_Balanescu,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.MistyMountains }
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Up the Pass",
                StageNumber = 1,
                Id = "0ec5f691-e5b3-41b5-bcec-2f963786ef4d",
                CardType = CardType.Quest,
                Setup = "stttttt",
                Text = "Setup: Add Caradhras to the staging area. Remove all copies of Snowstorm from the encounter deck and set them aside, out of play. Put Arwen Undomiel into play under the control of the first player.",
                OppositeTitle = "Up the Pass",
                OppositeText = "When Revealed: Reveal 1 card from the encounter deck per player, and add it to the staging area.",
                EncounterSet = "The Redhorn Gate",
                QuestPoints = 9,
                Quantity = 1,
                Number = 11,
                Artist = Artist.Jason_Juta,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.MistyMountains }
            });
        }
    }
}
