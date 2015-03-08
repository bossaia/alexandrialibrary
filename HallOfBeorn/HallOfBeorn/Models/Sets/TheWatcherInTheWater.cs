using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheWatcherintheWater : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Watcher in the Water";
            Abbreviation = "TWitW";
            Number = 11;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Dwarrowdelf";

            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Aragorn",
                Id = "51223bd0-ffd1-11df-a976-0801210c9001",
                CardType = CardType.Hero,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "D�nedain.", " Ranger." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Quantity = 1,
                ThreatCost = 12,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 2,
                HitPoints = 5,
                Text = "Refresh Action: Reduce your threat to your starting threat level. (Limit once per game.)",
                Keywords = new List<string>() { "Sentinel." },
                Number = 53,
                Artist = Artist.Tony_Foti
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Arwen Und�miel",
                NormalizedTitle = "Arwen Undomiel",
                Id = "51223bd0-ffd1-11df-a976-0801210c9002",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Noldor.", " Noble." },
                Quantity = 3,
                ResourceCost = 2,
                IsUnique = true,
                Attack = 0,
                Defense = 1,
                Willpower = 2,
                HitPoints = 2,
                Text = "Response: After Arwen Und�miel exhausts, choose a character. That character gains sentinel and gets +1 Defense until the end of the round.",
                Number = 58,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Disturbed Waters",
                Id = "51223bd0-ffd1-11df-a976-0801210c9003",
                CardType = CardType.Treachery,
                EncounterSet = "The Watcher in the Water",
                Quantity = 3,
                Keywords = new List<string>() { "Doomed 5." },
                Number = 71,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Doors of Durin",
                Id = "51223bd0-ffd1-11df-a976-0801210c9004",
                CardType = CardType.Location,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Gate." },
                Quantity = 1,
                IsUnique = true,
                Text = "Progress tokens that would be placed on Doors of Durin are instead placed on the current quest card.\r\nAction: Each player may discard any number of cards from his hand. Then, discard the top card of the encounter deck. If the first letter of the encounter card's title matches that of one of the discarded player cards, add Doors of Durin to your victory display. (Limit once per round.)",
                Threat = 2,
                QuestPoints = null,
                VictoryPoints = 3,
                Number = 65,
                Artist = Artist.Ben_Zweifel
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Elrond's Counsel",
                Id = "51223bd0-ffd1-11df-a976-0801210c9005",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 0,
                Text = "Action: If you control a unique Noldor character, give another character +1 Willpower until the end of the phase and lower your threat by 3.",
                Number = 59,
                Artist = Artist.Anthony_Palumbo
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Grasping Tentacle",
                Id = "51223bd0-ffd1-11df-a976-0801210c9006",
                CardType = CardType.Enemy,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Tentacle." },
                Quantity = 4,
                EasyModeQuantity = 3,
                EngagementCost = 12,
                Attack = 3,
                Defense = 0,
                HitPoints = 3,
                Text = "Forced: When Grasping Tentacle is attacked, discard the top card of the encounter deck. If that card has a shadow effect or is a Tentacle enemy, attach this card to an attacking character as a Tentacle attachment with the text: 'Attached character's Attack and Defense are reduced to 0.'",
                Threat = 2,
                Number = 73,
                Artist = Artist.Mark_Tarrisse
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Grave Cairn",
                Id = "51223bd0-ffd1-11df-a976-0801210c9007",
                CardType = CardType.Event,
                Sphere = Sphere.Leadership,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Response: After a character leaves play, add its Attack to another character's Attack until the end of the round.",
                Number = 54,
                Artist = Artist.Trudi_Castle
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Ill Purpose",
                Id = "51223bd0-ffd1-11df-a976-0801210c9008",
                CardType = CardType.Treachery,
                EncounterSet = "The Watcher in the Water",
                Quantity = 1,
                EasyModeQuantity = 0,
                Text = "When Revealed: All enemies in the staging area engage the player with the highest threat. Then, each player raises his threat by the total Threat of all cards in the staging area.",
                Shadow = "Shadow: attacking enemy gets +1 Attack. (+3 Attack instead if it is a Tentacle.)",
                Number = 70,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Legacy of Durin",
                Id = "51223bd0-ffd1-11df-a976-0801210c9009",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Condition." },
                Quantity = 3,
                ResourceCost = 1,
                IsUnique = true,
                Text = "Attach to a Dwarf hero.\r\nResponse: After you play a Dwarf character from your hand, draw 1 card.",
                Number = 61,
                Artist = Artist.Ilich_Henriquez
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Makeshift Passage",
                Id = "51223bd0-ffd1-11df-a976-0801210c9010",
                CardType = CardType.Location,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Swamp." },
                Quantity = 2,
                Text = "Forced: After you travel to Makeshift Passage, place 2 progress tokens on the current quest card, bypassing any active location.",
                FlavorText = "They reached the strip of dry land between the lake and the cliffs: it was narrow, often hardly a dozen yards across, and encumbered with fallen rock and stones; but they found a way, hugging the cliff, and keeping as far from the dark water as they might. - The Fellowship of the Ring",
                Threat = 1,
                QuestPoints = 5,
                Number = 68,
                Artist = Artist.Ben_Zweifel
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Perilous Swamp",
                Id = "51223bd0-ffd1-11df-a976-0801210c9011",
                CardType = CardType.Location,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Swamp." },
                Quantity = 2,
                Text = "No more than 1 progress token can be placed on Perilous Swamp each round.",
                Shadow = "Shadow: Remove 1 progress token from the current quest.",
                Threat = 4,
                QuestPoints = 2,
                Number = 67,
                Artist = Artist.Trudi_Castle
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Resourceful",
                Id = "51223bd0-ffd1-11df-a976-0801210c9012",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Quantity = 3,
                ResourceCost = 4,
                Text = "Attach to a hero you control.Attached hero collects 1 additional resource during the resource phase each round.",
                Keywords = new List<string>() { "Secrecy 3." },
                Number = 62,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Rivendell Bow",
                Id = "51223bd0-ffd1-11df-a976-0801210c9013",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Item.", " Weapon." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attach to a Noldor or Silvan character, or to Aragorn. Limit 1 per character.Attached character gains ranged.If attached character has a printed ranged keyword, it gets +1 Attack during a ranged attack.",
                Number = 57,
                Artist = Artist.Sara_Biddle
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Short Cut",
                Id = "51223bd0-ffd1-11df-a976-0801210c9014",
                CardType = CardType.Event,
                Sphere = Sphere.Lore,
                Quantity = 3,
                ResourceCost = 1,
                Text = "Response: After a location enters play, exhaust a Hobbit character to shuffle that location back into the encounter deck. Then, reveal 1 card from the encounter deck and add it to the staging area.",
                Number = 60,
                Artist = Artist.Igor_Kieryluk
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Stagnant Creek",
                Id = "51223bd0-ffd1-11df-a976-0801210c9015",
                CardType = CardType.Location,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Swamp." },
                Quantity = 3,
                EasyModeQuantity = 1,
                Text = "When Revealed: Discard the top card of the encounter deck. If the discarded card is a Tentacle enemy, add that card to the staging area and raise each player's threat by 5.",
                Threat = 3,
                QuestPoints = 3,
                Number = 69,
                Artist = Artist.David_Lecossu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Stair Falls",
                Id = "51223bd0-ffd1-11df-a976-0801210c9016",
                CardType = CardType.Location,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Stair." },
                Quantity = 1,
                IsUnique = true,
                Text = "Travel: The first player must exhaust 2 characters to travel here.",
                Shadow = "Shadow: Remove 1 progress token from the current quest.",
                Threat = 2,
                QuestPoints = 4,
                Number = 66,
                Artist = Artist.Lin_Bo
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Striking Tentacle",
                Id = "51223bd0-ffd1-11df-a976-0801210c9017",
                CardType = CardType.Enemy,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Tentacle." },
                Quantity = 4,
                EasyModeQuantity = 3,
                EngagementCost = 18,
                Attack = 4,
                Defense = 1,
                HitPoints = 3,
                Text = "Forced: When Striking Tentacle attacks, discard the top card of the encounter deck. If that card has a shadow effect or is a Tentacle enemy, this attack is considered undefended.",
                Threat = 2,
                Number = 75,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Sword that was Broken",
                Id = "51223bd0-ffd1-11df-a976-0801210c9018",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Artifact." },
                Quantity = 3,
                ResourceCost = 3,
                IsUnique = true,
                Text = "Attach to a hero. Attached hero gains a Leadership resource icon.\r\nIf attached hero is Aragorn, each character you control gets +1 Willpower.",
                Number = 55,
                Artist = Artist.Sara_Biddle
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Seething Lake",
                StageNumber = 2,
                Id = "51223bd0-ffd1-11df-a976-0801210c9019",
                CardType = CardType.Quest,
                EncounterSet = "The Watcher in the Water",
                Quantity = 1,
                FlavorText = "The others swung round and saw the waters of the lake seething, as if a host of snakes were swimming up from the southern end. - The Fellowship of the Ring\r\nThe Doors of Durin are blocked by an ancient spell. You must figure out a way into the mines before the Seething bog and its Watcher consumes you all.",
                OppositeText = 
@"When Revealed: Add The Watcher to the staging area. Doors of Durin becomes the active location, moving any previous active location to the staging area. Shuffle all Tentacle cards in the encounter discard pile back into the encounter deck.

If the players have at least 3 victory points and defeat this stage, they have won the game.",
                QuestPoints = 5,
                Number = 64,
                Artist = Artist.Adam_Schumpert,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.MistyMountains }
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Watcher",
                Id = "51223bd0-ffd1-11df-a976-0801210c9021",
                CardType = CardType.Enemy,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Creature.", " Tentacle." },
                Quantity = 1,
                EngagementCost = 48,
                IsUnique = true,
                Attack = 5,
                Defense = 7,
                HitPoints = 9,
                Text = "While there is another Tentacle enemy in play, The Watcher cannot be optionally engaged.\r\nIf The Watcher is in the staging area at the end of the combat phase, each player must deal 3 damage to 1 character he controls.",
                Keywords = new List<string>() { "Regenerate 2." },
                Threat = 4,
                VictoryPoints = 3,
                Number = 72,
                Artist = Artist.Matthew_Starbuck
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Thrashing Tentacle",
                Id = "51223bd0-ffd1-11df-a976-0801210c9022",
                CardType = CardType.Enemy,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Tentacle." },
                Quantity = 4,
                EasyModeQuantity = 3,
                EngagementCost = 12,
                Attack = 3,
                Defense = 0,
                HitPoints = 3,
                Text = "Forced: When Thrashing Tentacle is attacked, discard the top card of the encounter deck. If that card has a shadow effect or is a Tentacle enemy, deal the damage from the attack to 1 character an attacking player controls (ignoring defense).",
                Threat = 2,
                Number = 74,
                Artist = Artist.Cristi_Balanescu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "To the West-door",
                StageNumber = 1,
                Id = "51223bd0-ffd1-11df-a976-0801210c9023",
                CardType = CardType.Quest,
                EncounterSet = "The Watcher in the Water",
                Quantity = 1,
                QuestPoints = 13,
                Setup = "tt",
                Text = "Setup: Remove The Watcher and Doors of Durin from the encounter deck and set them aside, out of play.",
                FlavorText = "Elrond has asked you to scout the Mines of Moria on your return to Lorien, hoping to discover if it is the source of increased Orc activity along the Misty Mountains.",
                OppositeText = "When Revealed: Reveal cards from the encounter deck and add them to the staging area until there is at least X threat in the staging area. X is twice the number of players in the game.",
                OppositeFlavorText = "Your approach is blocked by a dark lake that slumbers beneath the face of the cliff. You must search for a way around the water.",
                Number = 63,
                Artist = Artist.Jason_Juta,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.MistyMountains }
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Watcher of the Bruinen",
                Id = "51223bd0-ffd1-11df-a976-0801210c9025",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Noldor.", " Warrior." },
                Quantity = 3,
                ResourceCost = 2,
                Attack = 1,
                Defense = 2,
                Willpower = 0,
                HitPoints = 2,
                Text = "Watcher of the Bruinen does not exhaust to defend.Forced: After Watcher of the Bruinen defends, either discard it from play or discard 1 card from your hand.",
                Keywords = new List<string>() { "Sentinel." },
                Number = 56,
                Artist = Artist.Kristina_Carroll
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Wrapped!",
                NormalizedTitle = "Wrapped",
                Id = "51223bd0-ffd1-11df-a976-0801210c9026",
                CardType = CardType.Treachery,
                EncounterSet = "The Watcher in the Water",
                Traits = new List<string>() { "Tentacle." },
                Quantity = 4,
                EasyModeQuantity = 2,
                Text = "When Revealed: The first player attaches Wrapped! to a hero he controls. (Counts as a Tentacle attachment with the text: 'Limit 1 per hero. Attached hero cannot exhaust or ready. At the end of the round, discard attached hero from play. Combat Action: Exhaust a hero you control without a Tentacle attachment to discard Wrapped!.')",
                Number = 76,
                Artist = Artist.Cristi_Balanescu
            });
        }
    }
}
