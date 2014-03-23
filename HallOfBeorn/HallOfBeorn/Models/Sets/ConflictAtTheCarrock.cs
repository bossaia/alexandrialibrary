using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class ConflictattheCarrock : CardSet
    {
        protected override void Initialize()
        {
            Name = "Conflict at the Carrock";
            Abbreviation = "CatC";
            Number = 3;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Shadows of Mirkwood";

            Cards.Add(new Card() {
                ImageName = "M1172",
                Title = "Frodo Baggins",
                Id = "51223bd0-ffd1-11df-a976-0801202c9001",
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 7,
                IsUnique = true,
                Attack = 1,
                Defense = 2,
                Willpower = 2,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Text = "Response: After Frodo ~Baggins is damaged, cancel the damage and instead raise your threat by the amount of damage he would have been dealt. (Limit once per phase.)",
                Quantity = 1,
                Number = 25
            });
            Cards.Add(new Card() {
                ImageName = "M1194",
                Title = "A Frightened Beast",
                Id = "51223bd0-ffd1-11df-a976-0801202c9002",
                CardType = CardType.Treachery,
                Quantity = 3,
                Text = "When Revealed: Each player raises his threat by the total Threat of all cards in the staging area. Any player may choose to discard from play 1 Creature ally card he controls to cancel this effect.",
                EncounterSet = "Conflict at the Carrock",
                Number = 47
            });
            Cards.Add(new Card() {
                ImageName = "M1183",
                Title = "Against the Trolls",
                StageNumber = 2,
                Id = "51223bd0-ffd1-11df-a976-0801202c9003",
                CardType = CardType.Quest,
                Quantity = 1,
                QuestPoints = 0,
                OppositeText = 
@"When Revealed: Place the unique Troll cards previously set aside into the staging area.

Players cannot defeat this stage if there are any Troll enemies in play.",
                EncounterSet = "Conflict at the Carrock",
                Number = 36
            });
            Cards.Add(new Card() {
                ImageName = "M1192",
                Title = "Bee Pastures",
                Id = "51223bd0-ffd1-11df-a976-0801202c9005",
                CardType = CardType.Location,
                Traits = new List<string>() { "Wilderlands." },
                Quantity = 3,
                Text = "Response: After you travel to Bee Pastures, search the encounter deck and discard pile for Grimbeorn the Old and add him to the staging area. Then, shuffle the encounter deck.",
                Threat = 1,
                QuestPoints = 2,
                EncounterSet = "Conflict at the Carrock",
                Number = 45
            });
            Cards.Add(new Card() {
                ImageName = "M1175",
                Title = "Beorning Beekeeper",
                Id = "51223bd0-ffd1-11df-a976-0801202c9006",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Beorning." },
                Quantity = 3,
                ResourceCost = 4,
                Attack = 2,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Text = "Action: Discard Beorning Beekeeper from play to deal 1 damage to each enemy in the staging area.",
                Number = 28
            });
            Cards.Add(new Card() {
                ImageName = "M1176",
                Title = "Born Aloft",
                Id = "51223bd0-ffd1-11df-a976-0801202c9007",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Condition." },
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: Discard Born Aloft from play to return attached ally to its owner's hand.",
                Keywords = new List<string>() { "Attach to an ally." },
                Number = 29
            });
            Cards.Add(new Card() {
                ImageName = "M1173",
                Title = "D�nedain Warning",
                NormalizedTitle = "Dunedain Warning",
                Id = "51223bd0-ffd1-11df-a976-0801202c9008",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Signal." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached hero gains +1 Defense.\r\nAction: Pay 1 resource from attached hero's pool to attach Dunedain Warning to another hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 26
            });
            Cards.Add(new Card() {
                ImageName = "M1177",
                Title = "�omund",
                NormalizedTitle = "Eomund",
                Id = "51223bd0-ffd1-11df-a976-0801202c9009",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Rohan." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Attack = 1,
                Defense = 1,
                Willpower = 2,
                HitPoints = 2,
                Text = "Response: After �omund leaves play, ready all Rohan characters in play.",
                Number = 30
            });
            Cards.Add(new Card() {
                ImageName = "M1180",
                Title = "A Burning Brand",
                Id = "51223bd0-ffd1-11df-a976-0801202c9010",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 2,
                Text = "While attached character is defending, cancel any shadow effects on cards dealt to the attacking enemy.",
                Keywords = new List<string>() { "Attach to a Lore character." },
                Number = 33
            });
            Cards.Add(new Card() {
                ImageName = "M1184",
                Title = "Grimbeorn the Old",
                Id = "51223bd0-ffd1-11df-a976-0801202c9011",
                CardType = CardType.Objective_Ally,
                Traits = new List<string>() { "Ally." },
                Quantity = 1,
                EasyModeQuantity = 1,
                NightmareModeQuantity = 0,
                IsUnique = true,
                Attack = 4,
                Defense = 3,
                Willpower = 2,
                HitPoints = 10,
                Text = 
@"Grimbeorn the Old does not exhaust to defend against Troll enemies.

If Grimbeorn the Old has 8 or more resource tokens on him, he joins the first player as an ally.

Action: Spend 1 Leadership resource to place that resource on Grimbeorn the Old.",
                EncounterSet = "Conflict at the Carrock",
                Number = 37
            });
            Cards.Add(new Card() {
                ImageName = "M1182",
                Title = "Grimbeorn's Quest",
                StageNumber = 1,
                Id = "51223bd0-ffd1-11df-a976-0801202c9012",
                CardType = CardType.Quest,
                QuestPoints = 7,
                Quantity = 1,
                Text = "Setup: Add The Carrock to the staging area. Remove 4 unique Troll cards and 4 copies of the 'Sacked!' card from the encounter deck and set them aside, out of play. Then shuffle 1 'Sacked!' card per player back into the encounter deck.",
                OppositeText = "Forced: After placing the 7th progress token on Grimbeorn's Quest, The Carrock becomes the active location. Discard the previous active location from play.",
                EncounterSet = "Conflict at the Carrock",
                Number = 35
            });
            Cards.Add(new Card() {
                ImageName = "M1179",
                Title = "Longbeard Map-Maker",
                Id = "51223bd0-ffd1-11df-a976-0801202c9014",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Dwarf." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Text = "Action: Spend 1 Lore resource to give Longbeard Map-Maker +1 Willpower until the end of the phase.",
                Number = 32
            });
            Cards.Add(new Card() {
                ImageName = "M1185",
                Title = "Louis",
                Id = "51223bd0-ffd1-11df-a976-0801202c9015",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 34,
                IsUnique = true,
                Attack = 4,
                Defense = 2,
                HitPoints = 10,
                Text = "While Louis is engaged with a player, all Troll enemies gain, \"Forced: After this enemy attacks, the defending player must raise his threat by 3.\"\r\n\r\nResponse: After defeating Louis, you may choose and discard 1 \"Sacked!\" card from play.",
                Threat = 2,
                EncounterSet = "Conflict at the Carrock",
                Number = 38
            });
            Cards.Add(new Card() {
                ImageName = "M1186",
                Title = "Morris",
                Id = "51223bd0-ffd1-11df-a976-0801202c9016",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 34,
                IsUnique = true,
                Attack = 4,
                Defense = 2,
                HitPoints = 10,
                Text = "While Morris is engaged with a player, all Troll enemies get +1 Attack.Response: After defeating Morris, you may choose and discard 1 'Sacked!' card from play.",
                Threat = 2,
                EncounterSet = "Conflict at the Carrock",
                Number = 39
            });
            Cards.Add(new Card() {
                ImageName = "M1189",
                Title = "Muck Adder",
                Id = "51223bd0-ffd1-11df-a976-0801202c9017",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature." },
                Quantity = 4,
                EngagementCost = 20,
                Attack = 2,
                Defense = 0,
                HitPoints = 4,
                Text = "Forced: If Muck Adder damages a character, discard that character from play.",
                Shadow = "Shadow: Defending character gets -1 Defense for the duration of this attack.",
                Threat = 1,
                EncounterSet = "Conflict at the Carrock",
                Number = 42
            });
            Cards.Add(new Card() {
                ImageName = "M1178",
                Title = "Nor am I a Stranger",
                Id = "51223bd0-ffd1-11df-a976-0801202c9018",
                CardType = CardType.Attachment,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Title." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached character gains the Rohan trait.",
                Keywords = new List<string>() { "Attach to a character." },
                Number = 31,
            });
            Cards.Add(new Card() {
                ImageName = "M1193",
                Title = "Oak-wood Grove",
                Id = "51223bd0-ffd1-11df-a976-0801202c9019",
                CardType = CardType.Location,
                Traits = new List<string>() { "Forest." },
                Quantity = 3,
                Text = "While Oak-wood Grove is the active location, resource tokens from any sphere may be spent as Leadership resource tokens.",
                Threat = 2,
                QuestPoints = 1,
                EncounterSet = "Conflict at the Carrock",
                Number = 46
            });
            Cards.Add(new Card() {
                ImageName = "M1191",
                Title = "River Langflood",
                Id = "51223bd0-ffd1-11df-a976-0801202c9020",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Quantity = 4,
                Text = "While it is in the staging area, River Langflood gets +1 Threat for each Troll enemy in play.",
                Threat = 2,
                QuestPoints = 3,
                EncounterSet = "Conflict at the Carrock",
                Number = 44
            });
            Cards.Add(new Card() {
                ImageName = "M1196",
                Title = "Roasted Slowly",
                Id = "51223bd0-ffd1-11df-a976-0801202c9021",
                CardType = CardType.Treachery,
                Quantity = 2,
                Text = "When Revealed: Destroy all heroes with the card 'Sacked!' attached. Then, shuffle Roasted Slowly back into the encounter deck.",
                Shadow = "Shadow: If attacking enemy is a Troll, remove 2 damage tokens from it.",
                EncounterSet = "Conflict at the Carrock",
                Number = 49
            });
            Cards.Add(new Card() {
                ImageName = "M1188",
                Title = "Rupert",
                Id = "51223bd0-ffd1-11df-a976-0801202c9022",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 34,
                IsUnique = true,
                Attack = 4,
                Defense = 2,
                HitPoints = 10,
                Text = "Forced: After Rupert attacks, shuffle all copies of the 'Sacked!' card from the discard pile back into the encounter deck.Response: After defeating Rupert, you may choose and discard 1 'Sacked!' card from play.",
                Threat = 2,
                EncounterSet = "Conflict at the Carrock",
                Number = 41
            });
            Cards.Add(new Card() {
                ImageName = "M1195",
                Title = "Sacked!",
                Id = "51223bd0-ffd1-11df-a976-0801202c9023",
                CardType = CardType.Treachery,
                Quantity = 5,
                Text = "When Revealed: Attach to a hero with no 'Sacked!' cards attached controlled by the first player. (Cannot be canceled.) Counts as a condition attachment with the text: 'Attached hero cannot attack, defend, commit to a quest, trigger its effect, or collect resources.'",
                Shadow = "Shadow: If attacking enemy is a Troll, resolve this card's 'when revealed' effect.",
                EncounterSet = "Conflict at the Carrock",
                Number = 48
            });
            Cards.Add(new Card() {
                ImageName = "M1174",
                Title = "Second Breakfast",
                Id = "51223bd0-ffd1-11df-a976-0801202c9024",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Each player returns the topmost attachment card from his discard pile to his hand.",
                Number = 27
            });
            Cards.Add(new Card() {
                ImageName = "M1181",
                Title = "Song of Wisdom",
                Id = "51223bd0-ffd1-11df-a976-0801202c9025",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached hero gains a Lore resource icon.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 34
            });
            Cards.Add(new Card() {
                ImageName = "M1187",
                Title = "Stuart",
                Id = "51223bd0-ffd1-11df-a976-0801202c9026",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Troll." },
                Quantity = 1,
                EngagementCost = 34,
                IsUnique = true,
                Attack = 4,
                Defense = 2,
                HitPoints = 10,
                Text = "While Stuart is engaged with a player, all Troll enemies get +1 Defense.Response: After defeating Stuart, you may choose and discard 1 'Sacked!' card from play.",
                Threat = 2,
                EncounterSet = "Conflict at the Carrock",
                Number = 40
            });
            Cards.Add(new Card() {
                ImageName = "M1190",
                Title = "The Carrock",
                Id = "51223bd0-ffd1-11df-a976-0801202c9027",
                CardType = CardType.Location,
                Traits = new List<string>() { "Riverland." },
                Quantity = 1,
                IsUnique = true,
                Text = "Players cannot travel to The Carrock except through quest card effects.While The Carrock is the active location, Troll enemies get +1 Attack and +1 Defense.",
                Keywords = new List<string>() { "Immune to player card effects." },
                Threat = 2,
                QuestPoints = 6,
                EncounterSet = "Conflict at the Carrock",
                Number = 43
            });
        }
    }
}
