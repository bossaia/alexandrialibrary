using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class TheDeadMarshes : CardSet
    {
        protected override void Initialize()
        {
            Name = "The Dead Marshes";
            Abbreviation = "TDM";
            Number = 6;
            SetType = Models.SetType.Adventure_Pack;
            Cycle = "Shadows of Mirkwood";

            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "A Wisp of Pale Sheen",
                Id = "51223bd0-ffd1-11df-a976-0801205c9001",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 3,
                EasyModeQuantity = 1,
                Text = "When Revealed: Place 2 resource tokens on Gollum. Any player may exhaust a Lore hero to reduce this effect to 1 resource token.Escape: 4",
                FlavorText = "Presently it grew altogether dark; the air itself seemed black and heavy to breathe. When lights appeared Sam rubbed his eyes: he thought his head was going queer. He first saw one with the corner of his left eye, a wisp of pale sheen that faded away... -The Two Towers",
                EncounterSet = "The Dead Marshes",
                Number = 108,
                Artist = Artist.Carolina_Eade
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Boromir",
                Id = "51223bd0-ffd1-11df-a976-0801205c9002",
                CardType = CardType.Hero,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Gondor.", " Noble.", " Warrior." },
                Quantity = 1,
                ThreatCost = 11,
                IsUnique = true,
                Attack = 3,
                Defense = 2,
                Willpower = 1,
                HitPoints = 5,
                Text = "Action: Raise your threat by 1 to ready Boromir.Action: Discard Boromir to deal 2 damage to each enemy engaged with a single player.",
                FlavorText = "\"...in Gondor we must trust to such weapons as we have.\" -The Fellowship of the Ring",
                Number = 95,
                Artist = Artist.Tiziano_Baracchi
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "D�nedain Cache",
                NormalizedTitle = "Dunedain Cache",
                Id = "51223bd0-ffd1-11df-a976-0801205c9003",
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "Item." },
                Quantity = 3,
                ResourceCost = 2,
                Text = "Attached hero gains ranged.\r\nAction: Pay 1 resource from attached hero's pool to attach Dunedain Cache to another hero.",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 97,
                Artist = Artist.Stephanie_M_Brown
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "D�nedain Watcher",
                NormalizedTitle = "Dunedain Watcher",
                Id = "51223bd0-ffd1-11df-a976-0801205c9004",
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                Traits = new List<string>() { "D�nedain." },
                NormalizedTraits = new List<string> { "Dunedain." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 2,
                Text = "Response: Discard Dunedain Watcher from play to cancel the shadow effects of a card just triggered.",
                FlavorText = "\"If simple folk are free from care and fear, simple they will be, and we must be secret to keep them so. That has been the task of my kindred, while the years have lengthened and the grass has grown.\" -Strider, The Fellowship of the Ring",
                Number = 96,
                Artist = Artist.Lindsey_Messecar
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Elfhelm",
                Id = "51223bd0-ffd1-11df-a976-0801205c9005",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                Traits = new List<string>() { "Rohan.", " Warrior." },
                Quantity = 3,
                ResourceCost = 4,
                IsUnique = true,
                Attack = 2,
                Defense = 2,
                Willpower = 1,
                HitPoints = 3,
                Text = "While Elfhelm is ready, he gains: 'Response: After your threat is raised as the result of questing unsuccessfully, or by an encounter or quest card effect, reduce your threat by 1.'",
                FlavorText = "\"Buy my lord sends word that we must set ourselves in readiness: orders may come for a sudden move.\" -The Return of the King",
                Number = 100,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Fast Hitch",
                Id = "51223bd0-ffd1-11df-a976-0801205c9006",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Skill." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust Fast Hitch to ready attached character.",
                FlavorText = "'And I put as fast a hitch over the stump as any one could have done, in the Shire or out of it.' -Sam, The Two Towers",
                Keywords = new List<string>() { "Attach to a Hobbit character." },
                Number = 103,
                Artist = Artist.David_A_Nash
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Fens and Mires",
                Id = "51223bd0-ffd1-11df-a976-0801205c9007",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dead Marshes." },
                Quantity = 4,
                Text = "Forced: After the players travel to this location, place 1 resource token on Gollum.Escape: 2",
                Threat = 2,
                QuestPoints = 2,
                EncounterSet = "The Dead Marshes",
                Number = 115,
                Artist = Artist.Stacey_Diana_Clark
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Giant Marsh Worm",
                Id = "51223bd0-ffd1-11df-a976-0801205c9008",
                CardType = CardType.Enemy,
                Traits = new List<string>() { "Creature." },
                Quantity = 4,
                EasyModeQuantity = 2,
                EngagementCost = 36,
                Attack = 3,
                Defense = 2,
                HitPoints = 6,
                Text = "Forced: Remove 2 damage from Giant Marsh Worm at the end of each round.Escape: 2",
                FlavorText = "\"No birds here. There are snakeses, wormses, things in the pools. Lot of things, lots of nasty things.\" -Gollum, The Two Towers",
                Threat = 1,
                EncounterSet = "The Dead Marshes",
                Number = 112,
                Artist = Artist.Jake_Murray
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Gollum",
                Id = "51223bd0-ffd1-11df-a976-0801205c9009",
                CardType = CardType.Objective,
                Traits = new List<string>() { "Gollum." },
                Quantity = 1,
                IsUnique = true,
                Text = "If Gollum ever has 8 or more resource tokens on him, shuffle him back into the encounter deck.Forced: At the end of the quest phase, the party must make an escape test, dealing 1 card per player from the encounter deck. If this test is failed, place 2 resource tokens on Gollum.",
                EncounterSet = "The Dead Marshes",
                Number = 107,
                Artist = Artist.John_Matson
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Impassable Bog",
                Id = "51223bd0-ffd1-11df-a976-0801205c9010",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dead Marshes." },
                Quantity = 4,
                EasyModeQuantity = 2,
                Text = "When Revealed: Place 1 resource token on Gollum for each location card in the staging area.Escape: 2",
                Threat = 1,
                QuestPoints = 12,
                EncounterSet = "The Dead Marshes",
                VictoryPoints = 7,
                Number = 113,
                Artist = Artist.Yoann_Boissonnet
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Into the Marshes",
                StageNumber = 1,
                Id = "51223bd0-ffd1-11df-a976-0801205c9011",
                CardType = CardType.Quest,
                QuestPoints = 12,
                Quantity = 1,
                Setup = "s",
                Text = "Setup: Search the encounter deck for Gollum, and add it to the staging area. Shuffle the encounter deck, then reveal 1 card per player from the encounter deck and add it to the staging area.",
                FlavorText = "You have followed Gollum's trail for days, and are now closing in fast pursuit. In a last ditch effort to escape you, the creature has fled to The Dead Marshes.",
                OppositeFlavorText = "'Yes, yes,' said Gollum. 'All dead, all rotten. Elves and Men and Orcs. The Dead Marshes. There was a great battle long ago, yes, so they told him when Smeagol was young, when I was young before teh Precious came. It was a great battle. Tall Men with long swords, and terrible Elves and Orcses shrieking. They fought on th eplain for days and months at the Black Gates. But the Marshes have grown since then, swallowed up the graves; always creeping, creeping.' -The Two Towers.",
                EncounterSet = "The Dead Marshes",
                Number = 105,
                Artist = Artist.Ignacio_Bazan_Lazcano,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.SauronsReach, EncounterSet.Wilderlands }
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Nightfall",
                Id = "51223bd0-ffd1-11df-a976-0801205c9013",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 3,
                Text = "When Revealed: The first player makes an escape test, dealing 2 cards from the encounter deck. If this test is failed, place 1 resource token on Gollum and raise each player's threat by 2.Escape: 2",
                EncounterSet = "The Dead Marshes",
                Number = 109,
                Artist = Artist.David_Lecossu
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Silvan Tracker",
                Id = "51223bd0-ffd1-11df-a976-0801205c9014",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                Traits = new List<string>() { "Silvan." },
                Quantity = 3,
                ResourceCost = 3,
                Attack = 1,
                Defense = 1,
                Willpower = 1,
                HitPoints = 3,
                Text = "Response: After a Silvan character readies during the refresh phase, heal 1 damage from that character.",
                FlavorText = "\"The Wood-elves tracked him first, an easy task for them, for his trail was still fresh then.\" -Gandalf, The Fellowship of the Rings",
                Number = 102,
                Artist = Artist.Magali_Villeneuve
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Song of Battle",
                Id = "51223bd0-ffd1-11df-a976-0801205c9015",
                CardType = CardType.Attachment,
                Sphere = Sphere.Neutral,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Attached hero gains a Tactics resource icon.",
                FlavorText = "We go, we go, we go to war, to hew the stone and break the door. -The Two Towers",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 104,
                Artist = Artist.Leonardo_Borazio
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Song of Mocking",
                Id = "51223bd0-ffd1-11df-a976-0801205c9016",
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Song." },
                Quantity = 3,
                ResourceCost = 1,
                Text = "Action: Exhaust Song of Mocking to choose another hero. Until the end of the phase, attached hero takes all damage assigned to the chosen hero.",
                FlavorText = @"Old fat spider spinning in a tree!
Old fat spider can't see me!
Attercop! Attercop!
Won't you stop. Stop your spinning and look for me! 
-Bilbo Baggins, The Hobbit ",
                Keywords = new List<string>() { "Attach to a hero." },
                Number = 99,
                Artist = Artist.Carolina_Eade
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Capture",
                StageNumber = 2,
                Id = "51223bd0-ffd1-11df-a976-0801205c9017",
                CardType = CardType.Quest,
                FlavorText = "After a tiring pursuit through the treacherous marshland, you have cornered Gollum, and move in for the capture.",
                OppositeText = 
@"Forced: After this stage is defeated, the first player chooses a player. That player must pass an Escape test, dealing 1 card from the encounter deck for each resource token on Gollum, to capture him. If Gollum is not captured at this time, reset the quest deck to stage 1B.

If this final Escape test is passed, the players have captured Gollum and won the game.",
                QuestPoints = 3,
                Quantity = 1,
                EncounterSet = "The Dead Marshes",
                Number = 106,
                Artist = Artist.Ignacio_Bazan_Lazcano,
                IncludedEncounterSets = new List<EncounterSet> { EncounterSet.SauronsReach, EncounterSet.Wilderlands }
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Heart of the Marshes",
                Id = "51223bd0-ffd1-11df-a976-0801205c9019",
                CardType = CardType.Location,
                Traits = new List<string>() { "Dead Marshes." },
                Quantity = 4,
                EasyModeQuantity = 2,
                Text = "While The Heart of the Marshes is the active location, all cards dealt from the encounter deck for escape tests get +1 Escape. (Cards receive this bonus even if they do not have a printed escape value.)Escape: 1",
                Threat = 3,
                QuestPoints = 4,
                EncounterSet = "The Dead Marshes",
                Number = 114,
                Artist = Artist.Noah_Bradley
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "The Lights of the Dead",
                Id = "51223bd0-ffd1-11df-a976-0801205c9020",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 4,
                EasyModeQuantity = 2,
                Text = "When Revealed: Each player must make an escape test, dealing 2 cards from the encounter deck for each test. Each player that fails this test places 1 resource token on Gollum, and raises his threat by 1.Escape: 5",
                EncounterSet = "The Dead Marshes",
                Number = 111,
                Artist = Artist.Henning_Ludvigsen
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Through the Mist",
                Id = "51223bd0-ffd1-11df-a976-0801205c9021",
                CardType = CardType.Treachery,
                Traits = new List<string>() { "Escape." },
                Quantity = 3,
                Text = "When Revealed: The first player makes an escape test counting Attack instead of Willpower, dealing 2 cards from the encounter deck. If this test is failed, place 1 resource token on Gollum and raise each player's threat by 1.Escape: 3",
                EncounterSet = "The Dead Marshes",
                Number = 110,
                Artist = Artist.Igor_Kieryluk
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "Vassal of the Windlord",
                Id = "51223bd0-ffd1-11df-a976-0801205c9022",
                CardType = CardType.Ally,
                Sphere = Sphere.Tactics,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Quantity = 3,
                ResourceCost = 1,
                Attack = 3,
                Defense = 0,
                Willpower = 0,
                HitPoints = 1,
                Text = "Vassal of the Windlord cannot have restricted attachments.Forced: After an attack in which Vassal of the Windlord attacked resolves, discard Vassal of the Windlord from play.",
                Keywords = new List<string>() { "Ranged." },
                Number = 98,
                Artist = Artist.Anna_Christenson
            });
            Cards.Add(new Card() {
                ImageType = Models.ImageType.Png,
                Title = "We Do Not Sleep",
                Id = "51223bd0-ffd1-11df-a976-0801205c9023",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                Quantity = 3,
                ResourceCost = 5,
                Text = "Action: Until the end of the phase, Rohan characters do not exhaust to commit to quests.",
                FlavorText = "\"But we must ride more warily; for war is abroad, and the Rohirrim, the Horse-lords, do not sleep, even if it seems so from afar.\" -Gandalf, The Two Towers",
                Number = 101,
                Artist = Artist.Lin_Bo
            });
        }
    }
}
