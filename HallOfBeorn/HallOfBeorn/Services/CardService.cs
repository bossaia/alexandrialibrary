﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HallOfBeorn.Models;
using HallOfBeorn.Models.Decks.HallOfBeorn;
using HallOfBeorn.Models.Decks.TalesFromTheCards;
using HallOfBeorn.Models.Sets;

namespace HallOfBeorn.Services
{
    public class CardService
    {
        public CardService()
        {
            LoadSets();
            LoadDecks();
            LoadRelationships();

            //NOTE: These traits are referenced by scenarios but not included on a player card or encounter card as a trait
            traits["Captive."] = "Captive.";
        }

        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly List<string> setNames = new List<string>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();
        private readonly Dictionary<string, string> keywords = new Dictionary<string, string>();
        private readonly Dictionary<string, string> traits = new Dictionary<string, string>();
        private readonly List<Deck> decks = new List<Deck>();

        const int maxResults = 128;
        const string randomKeyword = "random";

        private void AddSet(CardSet cardSet)
        {
            sets.Add(cardSet);

            if (!string.IsNullOrEmpty(cardSet.Cycle) && !setNames.Contains(cardSet.Cycle.ToUpper()))
                setNames.Add(cardSet.Cycle.ToUpper());

            setNames.Add(cardSet.Name);

            foreach (var card in cardSet.Cards)
            {
                cards.Add(card.Id, card);

                foreach (var keyword in card.Keywords)
                {
                    var keywordKey = keyword.Trim();
                    var keywordValue = keywordKey.Replace("~", string.Empty);
                    if (!keywords.ContainsKey(keywordKey))
                        keywords.Add(keywordKey, keywordValue);
                }

                foreach (var trait in card.Traits)
                {
                    var traitKey = trait.Replace(".", string.Empty).Trim();
                    if (!traits.ContainsKey(traitKey))
                        traits.Add(traitKey, trait.Trim());
                }
            }
        }

        private void AddDeck(Deck deck)
        {
            decks.Add(deck);

            if (string.IsNullOrEmpty(deck.DeckList))
                return;

            foreach (var line in deck.DeckList.Split(new string [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tokens = line.Split(' ').ToList();
                if (tokens == null || tokens.Count == 0)
                    continue;

                if (tokens.Last().StartsWith("x"))
                {
                    tokens.Remove(tokens.Last());
                }

                var setName = string.Empty;

                var last = tokens.LastOrDefault();
                if (last != null && last.Contains('(') && last.Contains(')'))
                {
                    setName = last.Trim().TrimStart('(').TrimEnd(')');
                    tokens.Remove(last);
                }

                if (tokens.Count == 0)
                    continue;

                var title = string.Join(" ", tokens).ToLower();

                var card = cards.Values.Where(x => 
                    (
                        (string.Equals(x.Title.ToLower(), title) || (!string.IsNullOrEmpty(x.NormalizedTitle) && string.Equals(x.NormalizedTitle.ToLower(), title)))
                        && ( string.IsNullOrEmpty(setName) || ( string.Equals(x.CardSet.Abbreviation, setName) || string.Equals(x.CardSet.Name, setName) ) )
                        && ( x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event || x.CardType == CardType.Treasure || x.CardType == CardType.Boon )
                    )
                ).FirstOrDefault();

                if (card != null)
                {
                    card.Decks.Add(deck);
                }
            }
        }

        private void LoadSets()
        {
            AddSet(new CoreSet());
            //AddSet(new CoreSetNightmare());

            AddSet(new TheHuntforGollum());
            AddSet(new ConflictattheCarrock());
            AddSet(new AJourneytoRhosgobel());
            AddSet(new TheHillsofEmynMuil());
            AddSet(new TheDeadMarshes());
            AddSet(new ReturntoMirkwood());

            AddSet(new Khazaddum());

            AddSet(new TheRedhornGate());
            AddSet(new RoadtoRivendell());
            AddSet(new TheWatcherintheWater());
            AddSet(new TheLongDark());
            AddSet(new FoundationsofStone());
            AddSet(new ShadowandFlame());

            AddSet(new HeirsofNumenor());

            AddSet(new TheStewardsFear());
            AddSet(new TheDruadanForest());
            AddSet(new EncounteratAmonDin());
            AddSet(new AssaultonOsgiliath());
            AddSet(new TheBloodofGondor());
            AddSet(new TheMorgulVale());

            AddSet(new VoiceofIsengard());
            AddSet(new TheDunlandTrap());

            AddSet(new TheHobbitOverHillandUnderHill());
            AddSet(new TheHobbitOntheDoorstep());
            AddSet(new TheBlackRiders());

            AddSet(new TheMassingatOsgiliath());
            AddSet(new TheBattleofLakeTown());
            AddSet(new TheStoneofErech());

            AddSet(new PassageThroughMirkwoodnNightmare());
            AddSet(new JourneyAlongTheAnduinNightmare());
            AddSet(new EscapeFromDolGuldurNightmare());

            AddSet(new TheHuntForGollumNightmare());
            AddSet(new ConflictAtTheCarrockNightmare());
            AddSet(new AJourneyToRhosgobelNightmare());
            AddSet(new TheHillsOfEmynMuilNightmare());
            AddSet(new TheDeadMarshesNightmare());
            AddSet(new ReturnToMirkwoodNightmare());
            AddSet(new IntoThePitNightmare());
            AddSet(new TheSeventhLevelNightmare());
            AddSet(new FlightFromMoriaNightmare());
            AddSet(new WeMustAwayEreBreakOfDayNightmare());
            AddSet(new OverTheMistyMountainsGrimNightmare());
            AddSet(new DungeonsDeepAndCavernsDimNightmare());
        }

        private void LoadDecks()
        {
            AddDeck(new WarriorsOfTheWhiteTower());
            AddDeck(new TheSpiritOfGondor());
            AddDeck(new FaramirAndTheRangersOfIthilien());
            AddDeck(new BoromirLeadsTheCharge());
            AddDeck(new CaldarasSacrifice());
            AddDeck(new KeepItSecretKeepItSafe());
            AddDeck(new RangersAndTraps());
            AddDeck(new SecretsOfTheWise());
            AddDeck(new CalledToTheSea());
            AddDeck(new PrisonerOfTheDarkForest());
            AddDeck(new ReclaimingKhazadDum());
            AddDeck(new ThreeKingsAQueenAndAPrince());
            AddDeck(new TheGreyCompanyDefendsGondor());
            AddDeck(new MagaliTribute());
            AddDeck(new BeornAttacks());
            AddDeck(new MastersOfTheForest());
            AddDeck(new TheFieldOfCormallen());
            AddDeck(new IsildursHeir());
            AddDeck(new FlightToTheFord());
            AddDeck(new TwoKingdomsReunited());
            AddDeck(new HamaTakesArcheryLessons());
            AddDeck(new BardGoesHunting());
            AddDeck(new TheIslandOfMisfitHeroes());
            AddDeck(new BalinHoldsTheLine());
            AddDeck(new TheRohirrimRideWithTheGreyCompany());
            AddDeck(new ThePowerOfThePalantir());
            AddDeck(new LoreMastery());
            AddDeck(new EaglesAndHorsesAndBearsOhMy());
            AddDeck(new TheDwarvesAndFaramir());
            AddDeck(new LocationControl());
            AddDeck(new Gluttony());
            AddDeck(new BoromirAndTheSevenDwarves());
            AddDeck(new VilyaTheRingOfAir());
            AddDeck(new DirectDamageTacticsLeadership());
            AddDeck(new DirectDamageLeadershipSpiritLore());
            AddDeck(new RenewedFriendshipsDwarvesElvesAndMen());
            AddDeck(new RenewedFriendshipsElvesAndElfFriends());
            AddDeck(new BattleOfPelennorRideToRuin());
            AddDeck(new BattleOfPelennorAndTheWorldsEnding());
            AddDeck(new SecretsOfErebor());
            AddDeck(new SecretsOfErebor2());
            AddDeck(new TheOrcHuntersOfImladris());
            AddDeck(new WardensOfImladris());

            AddDeck(new RideToRuin());
            AddDeck(new SpearmanSuperhero());
            AddDeck(new PalantirSupport());
            AddDeck(new TrapsOfIthilien());
            AddDeck(new EleanorsBigAdventure());
            AddDeck(new OutlandsGoneWild());
            AddDeck(new BlazeOfGlory());
            AddDeck(new RidingWithRohan());
            AddDeck(new WhereEaglesDare());

            AddDeck(new BeornsPathPTMLeadershipLore());
            AddDeck(new BeornsPathJAtALeadershipLore());
            AddDeck(new BeornsPathEFDGTacticsSpirit());
            AddDeck(new BeornsPathTHFGLeadershipLore());
            AddDeck(new BeornsPathTHFGTacticsSpirit());
            AddDeck(new BeornsPathCatCLeadershipLore());
            AddDeck(new BeornsPathAJtRLeadershipLore());
            AddDeck(new BeornsPathTHoEMLeadershipLore());
            AddDeck(new BeornsPathTDMLeadershipLore());
            AddDeck(new BeornsPathRtMTacticsSpirit());
        }

        private void AddRelationship(string leftTitle, string leftSet, string rightTitle, string rightSet)
        {
            var leftCard = cards.Values.Where(x => (string.Equals(x.Title, leftTitle) || string.Equals(x.NormalizedTitle, leftTitle)) && (string.IsNullOrEmpty(leftSet) || string.Equals(x.CardSet.Abbreviation, leftSet))).FirstOrDefault();
            var rightCard = cards.Values.Where(x => (string.Equals(x.Title, rightTitle) || string.Equals(x.NormalizedTitle, rightTitle)) && (string.IsNullOrEmpty(rightSet) || string.Equals(x.CardSet.Abbreviation, rightSet))).FirstOrDefault();

            if (leftCard == null || rightCard == null)
                return;

            if (!leftCard.RelatedCards.Any(x => x.Id == rightCard.Id))
                leftCard.RelatedCards.Add(rightCard);

            if (!rightCard.RelatedCards.Any(x => x.Id == leftCard.Id))
                rightCard.RelatedCards.Add(leftCard);
        }

        private void LoadRelationships()
        {
            AddRelationship("Aragorn", "Core", "Aragorn", "TWitW");
            AddRelationship("Aragorn", "Core", "Faramir", "Core");
            AddRelationship("Aragorn", "Core", "Steward of Gondor", "Core");
            AddRelationship("Aragorn", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Aragorn", "Core", "Sword that was Broken", "TWitW");
            AddRelationship("Aragorn", "Core", "Ring of Barahir", "TSF");

            AddRelationship("Theodred", "Core", "Snowbourn Scout", "Core");
            AddRelationship("Theodred", "Core", "Steward of Gondor", "Core");
            AddRelationship("Theodred", "Core", "Aragorn", "Core");

            AddRelationship("Gloin", "Core", "Lure of Moria", "RtR");
            AddRelationship("Gloin", "Core", "Longbeard Elder", "FoS");
            AddRelationship("Gloin", "Core", "We Are Not Idle", "SaF");
            AddRelationship("Gloin", "Core", "Cram", "THOHaUH");
            AddRelationship("Gloin", "Core", "Gloin", "THOtD");

            AddRelationship("Gimli", "Core", "Citadel Plate", "Core");
            AddRelationship("Gimli", "Core", "Horn of Gondor", "Core");
            AddRelationship("Gimli", "Core", "Feint", "Core");
            AddRelationship("Gimli", "Core", "Quick Strike", "Core");
            AddRelationship("Gimli", "Core", "Swift Strike", "Core");
            AddRelationship("Gimli", "Core", "Thalin", "Core");

            AddRelationship("Legolas", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Legolas", "Core", "Rivendell Blade", "RtR");
            AddRelationship("Legolas", "Core", "Hands Upon the Bow", "SaF");
            AddRelationship("Legolas", "Core", "Foe-hammer", "THOHaUH");
            AddRelationship("Legolas", "Core", "Horn of Gondor", "Core");

            AddRelationship("Thalin", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Thalin", "Core", "Feint", "Core");
            AddRelationship("Thalin", "Core", "Swift Strike", "Core");
            AddRelationship("Thalin", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Thalin", "Core", "Horn of Gondor", "Core");
            AddRelationship("Thalin", "Core", "Gimli", "Core");

            AddRelationship("Eowyn", "Core", "Northern Tracker", "Core");
            AddRelationship("Eowyn", "Core", "A Test of Will", "Core");
            AddRelationship("Eowyn", "Core", "Unexpected Courage", "Core");
            AddRelationship("Eowyn", "Core", "Gandalf", "Core");
            AddRelationship("Eowyn", "Core", "Dunhere", "Core");

            AddRelationship("Faramir", "Core", "Steward of Gondor", "Core");
            AddRelationship("Faramir", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Faramir", "Core", "Faramir", "AoO");

            AddRelationship("Longbeard Orc Slayer", "Core", "Sneak Attack", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Steward of Gondor", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Gandalf", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Lure of Moria", "RtR");
            AddRelationship("Longbeard Orc Slayer", "Core", "Longbeard Elder", "FoS");
            AddRelationship("Longbeard Orc Slayer", "Core", "We Are Not Idle", "SaF");
            AddRelationship("Longbeard Orc Slayer", "Core", "Miner of the Iron Hills", "Core");

            AddRelationship("Sneak Attack", "Core", "Gandalf", "Core");

            AddRelationship("Steward of Gondor", "Core", "Sneak Attack", "Core");
            AddRelationship("Steward of Gondor", "Core", "Gandalf", "Core");

            AddRelationship("Celebrian's Stone", "Core", "Steward of Gondor", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Aragorn", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Aragorn", "TWitW");
            AddRelationship("Celebrian's Stone", "Core", "Faramir", "Core");
            
            AddRelationship("Gondorian Spearman", "Core", "Feint", "Core");
            AddRelationship("Gondorian Spearman", "Core", "Gandalf", "Core");
            AddRelationship("Gondorian Spearman", "Core", "Horn of Gondor", "Core");
            
            AddRelationship("Feint", "Core", "Quick Strike", "Core");
            AddRelationship("Feint", "Core", "Hama", "TLD");

            AddRelationship("Quick Strike", "Core", "Gimli", "Core");
            AddRelationship("Quick Strike", "Core", "Feint", "Core");
            AddRelationship("Quick Strike", "Core", "Swift Strike", "Core");

            AddRelationship("Swift Strike", "Core", "Feint", "Core");
            AddRelationship("Swift Strike", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Swift Strike", "Core", "Thalin", "Core");

            AddRelationship("Blade of Gondolin", "Core", "Legolas", "Core");
            AddRelationship("Blade of Gondolin", "Core", "Foe-hammer", "THOHaUH");
            AddRelationship("Blade of Gondolin", "Core", "Goblin-cleaver", "THOHaUH");
            
            AddRelationship("Citadel Plate", "Core", "Gimli", "Core");
            AddRelationship("Citadel Plate", "Core", "Horn of Gondor", "Core");

            AddRelationship("Horn of Gondor", "Core", "Sneak Attack", "Core");
            AddRelationship("Horn of Gondor", "Core", "Prince Imrahil", "AJtR");
            
            AddRelationship("The Galadhrim's Greeting", "Core", "Gandalf", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "Gandalf", "THOHaUH");
            
            AddRelationship("Hasty Stroke", "Core", "A Test of Will", "Core");
            AddRelationship("Hasty Stroke", "Core", "A Burning Brand", "CatC");

            AddRelationship("A Test of Will", "Core", "Unexpected Courage", "Core");
            AddRelationship("A Test of Will", "Core", "Gandalf", "Core");
            
            AddRelationship("Stand and Fight", "Core", "Eowyn", "Core");
            AddRelationship("Stand and Fight", "Core", "Horn of Gondor", "Core");
            AddRelationship("Stand and Fight", "Core", "Dwarven Tomb", "Core");
            AddRelationship("Stand and Fight", "Core", "Escort from Edoras", "AJtR");

            AddRelationship("Dwarven Tomb", "Core", "A Test of Will", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Hasty Stroke", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Elrond's Counsel", "SaF");
            AddRelationship("Dwarven Tomb", "Core", "The Galadhrim's Greeting", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Escort from Edoras", "AJtR");
            AddRelationship("Dwarven Tomb", "Core", "Miruvor", "SaF");

            AddRelationship("Unexpected Courage", "Core", "A Test of Will", "Core");
            AddRelationship("Unexpected Courage", "Core", "Imladris Stargazer", "FoS");

            AddRelationship("Erebor Hammersmith", "Core", "Gleowine", "Core");

            AddRelationship("Henamarth Riversong", "Core", "Denethor", "Core");
            AddRelationship("Henamarth Riversong", "Core", "Rumour from the Earth", "RtM");

            AddRelationship("Miner of the Iron Hills", "Core", "Erebor Hammersmith", "Core");

            AddRelationship("Beravor", "Core", "Aragorn", "TWitW");
            AddRelationship("Gleowine", "Core", "Beravor", "Core");
            
            AddRelationship("Radagast's Cunning", "Core", "Secret Paths", "Core");
            
            AddRelationship("Secret Paths", "Core", "Radagast's Cunning", "Core");
            
            AddRelationship("Forest Snare", "Core", "Protector of Lorien", "Core");
            AddRelationship("Forest Snare", "Core", "Anborn", "TBoG");

            AddRelationship("Protector of Lorien", "Core", "Gleowine", "Core");
            AddRelationship("Protector of Lorien", "Core", "A Burning Brand", "CatC");
            AddRelationship("Protector of Lorien", "Core", "Beravor", "Core");

            AddRelationship("Rivendell Minstrel", "THfG", "Song of Kings", "THfG");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Travel", "THoEM");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Battle", "TDM");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Wisdom", "CatC");
            AddRelationship("Rivendell Minstrel", "THfG", "Gleowine", "Core");
            
            AddRelationship("Westfold Horse-Breaker", "THfG", "Eowyn", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "A Test of Will", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "Unexpected Courage", "Core");

            AddRelationship("Winged Guardian", "THfG", "Horn of Gondor", "Core");
            AddRelationship("Winged Guardian", "THfG", "The Eagles Are Coming!", "THfG");
            AddRelationship("Winged Guardian", "THfG", "Vassal of the Windlord", "TDM");
            AddRelationship("Winged Guardian", "THfG", "Eagles of the Misty Mountains", "RtM");
            AddRelationship("Winged Guardian", "THfG", "Descendant of Thorondor", "THoEM");
            AddRelationship("Winged Guardian", "THfG", "Landroval", "AJtR");

            AddRelationship("Frodo Baggins", "CatC", "Fast Hitch", null);
            AddRelationship("Frodo Baggins", "CatC", "The Galadhrim's Greeting", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Aragorn", "TWitW");
            AddRelationship("Frodo Baggins", "CatC", "Wandering Took", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Gandalf", "Core");

            AddRelationship("Dunedain Warning", "CatC", "Dunedain Quest", null);
            AddRelationship("Dunedain Warning", "CatC", "Dunedain Mark", null);
            AddRelationship("Dunedain Warning", "CatC", "Dunedain Cache", null);
            AddRelationship("Dunedain Warning", "CatC", "Dunedain Signal", null);

            AddRelationship("Haldir of Lorien", "AJtR", "Mirlonde", "TDF");
            AddRelationship("Haldir of Lorien", "AJtR", "Gildor Inglorion", "THoEM");
            AddRelationship("Haldir of Lorien", "AJtR", "Protector of Lorien", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "A Burning Brand", "CatC");
            AddRelationship("Haldir of Lorien", "AJtR", "Silvan Tracker", "TDM");
            AddRelationship("Haldir of Lorien", "AJtR", "Mirkwood Runner", "RtM");
            AddRelationship("Haldir of Lorien", "AJtR", "Henamarth Riversong", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "Daughter of the Nimrodel", "Core");

            AddRelationship("Ancient Mathom", "AJtR", "Northern Tracker", "Core");
            AddRelationship("Ancient Mathom", "AJtR", "The Riddermark's Finest", "THoEM");
            AddRelationship("Ancient Mathom", "AJtR", "Asfaloth", "FoS");
            AddRelationship("Ancient Mathom", "AJtR", "West Road Traveller", "RtM");

            AddRelationship("Escort from Edoras", "AJtR", "Horn of Gondor", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "A Test of Will", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "Ancient Mathom", "AJtR");
            AddRelationship("Escort from Edoras", "AJtR", "West Road Traveller", "RtM");

            AddRelationship("Descendant of Thorondor", "THoEM", "Winged Guardian", "THfG");
            AddRelationship("Descendant of Thorondor", "THoEM", "Radagast", "AJtR");
            AddRelationship("Descendant of Thorondor", "THoEM", "Vassal of the Windlord", "TDM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Eagles of the Misty Mountains", "RtM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Hail of Stones", "RtR");
            AddRelationship("Descendant of Thorondor", "THoEM", "Sneak Attack", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Horn of Gondor", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Born Aloft", "CatC");
            AddRelationship("Descendant of Thorondor", "THoEM", "Gondorian Spearman", "Core");

            AddRelationship("Gildor Inglorion", "THoEM", "Gleowine", "Core");
            AddRelationship("Gildor Inglorion", "THoEM", "Haldir of Lorien", "AJtR");
            AddRelationship("Gildor Inglorion", "THoEM", "Asfaloth", "FoS");
            AddRelationship("Gildor Inglorion", "THoEM", "A Burning Brand", "CatC");

            AddRelationship("Song of Travel", "THoEM", "Song of Battle", "");
            AddRelationship("Song of Travel", "THoEM", "Song of Kings", "");
            AddRelationship("Song of Travel", "THoEM", "Song of Wisdome", "");
            
            AddRelationship("Elfhelm", "TDM", "Eowyn", "Core");
            AddRelationship("Elfhelm", "TDM", "Dunhere", "Core");
            AddRelationship("Elfhelm", "TDM", "Eomund", "CatC");
            AddRelationship("Elfhelm", "TDM", "Escort from Edoras", "AJtR");
            AddRelationship("Elfhelm", "TDM", "Westfold Horse-Breaker", "THfG");
            AddRelationship("Elfhelm", "TDM", "The Riddermark's Finest", "THoEM");

            AddRelationship("Fast Hitch", "TDM", "Unexpected Courage", "Core");
            AddRelationship("Fast Hitch", "TDM", "Peace, and Thought", "SaF");

            AddRelationship("Silvan Tracker", "TDM", "Haldir of Lorien", "AJtR");
            AddRelationship("Silvan Tracker", "TDM", "Mirlonde", "TDF");
            AddRelationship("Silvan Tracker", "TDM", "Daughter of the Nimrodel", "Core");
            AddRelationship("Silvan Tracker", "TDM", "Elrond", "SaF");
            AddRelationship("Silvan Tracker", "TDM", "Mirkwood Runner", "RtM");

            AddRelationship("Vassal of the Windlord", "TDM", "Sneak Attack", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Steward of Gondor", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Gondorian Spearman", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Feint", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Gandalf", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Horn of Gondor", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Winged Guardian", "THfG");
            AddRelationship("Vassal of the Windlord", "TDM", "Eagles of the Misty Mountains", "RtM");
            
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Horn of Gondor", "Core");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Gandalf", "Core");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "The Eagles Are Coming!", "THfG");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Winged Guardian", "THfG");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Landroval", "AJtR");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Vassal of the Windlord", "TDM");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Support of the Eagles", "RtM");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Radagast", "AJtR");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Descendant of Thorondor", "THoEM");

            AddRelationship("Bifur", "KD", "Bifur", "THOtD");

            AddRelationship("Erebor Record Keeper", "KD", "Miner of the Iron Hills", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Lure of Moria", "RtR");
            AddRelationship("Erebor Record Keeper", "KD", "Legacy of Durin", "TWitW");
            AddRelationship("Erebor Record Keeper", "KD", "Daeron's Runes", "FoS");
            AddRelationship("Erebor Record Keeper", "KD", "We Are Not Idle", "SaF");
            AddRelationship("Erebor Record Keeper", "KD", "Fili", "THOHaUH");
            AddRelationship("Erebor Record Keeper", "KD", "Kili", "THOHaUH");
            AddRelationship("Erebor Record Keeper", "KD", "Gloin", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Erebor Hammersmith", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Longbeard Map-Maker", "CatC");
            AddRelationship("Erebor Record Keeper", "KD", "Bifur", "KD");

            AddRelationship("Narvi's Belt", "KD", "Thorin Oakenshield", "THOtD");
            AddRelationship("Narvi's Belt", "KD", "Dain Ironfoot", "RtM");
            AddRelationship("Narvi's Belt", "KD", "Thorin Oakenshield", "THOtD");
            AddRelationship("Narvi's Belt", "KD", "Gloin", "Core");
            AddRelationship("Narvi's Belt", "KD", "Balin", "THOtD");

            AddRelationship("Zigil Miner", "KD", "Imladris Stargazer", "FoS");
            AddRelationship("Zigil Miner", "KD", "Hidden Cache", "TMV");

            AddRelationship("Bofur", "TRG", "Bofur", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Zigil Miner", "KD");
            AddRelationship("Bofur", "TRG", "Fili", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Kili", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Horn of Gondor", "Core");
            
            AddRelationship("Hail of Stones", "RtR", "Gondorian Spearman", "Core");
            AddRelationship("Hail of Stones", "RtR", "Horn of Gondor", "Core");
            AddRelationship("Hail of Stones", "RtR", "Winged Guardian", "THfG");
            AddRelationship("Hail of Stones", "RtR", "Vassal of the Windlord", "TDM");
            AddRelationship("Hail of Stones", "RtR", "Thalin", "Core");
            AddRelationship("Hail of Stones", "RtR", "Hands Upon the Bow", "SaF");

            AddRelationship("Lure of Moria", "RtR", "Erebor Record Keeper", "KD");
            AddRelationship("Lure of Moria", "RtR", "We Are Not Idle", "SaF");
            AddRelationship("Lure of Moria", "RtR", "Fili", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Kili", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Longbeard Orc Slayer", "Core");
            AddRelationship("Lure of Moria", "RtR", "Longbeard Elder", "FoS");
            AddRelationship("Lure of Moria", "RtR", "Cram", "THOHaUH");
            
            AddRelationship("Rivendell Blade", "RtR", "Legolas", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Blade of Gondolin", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Elladan", "RtR");
            AddRelationship("Rivendell Blade", "RtR", "Elrohir", "TRG");
            AddRelationship("Rivendell Blade", "RtR", "Glorfindel", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Glorfindel", "FoS");
            AddRelationship("Rivendell Blade", "RtR", "Rivendell Bow", "TWitW");
            AddRelationship("Rivendell Blade", "RtR", "Mirlonde", "TDF");
            AddRelationship("Rivendell Blade", "RtR", "Elrond", "SaF");
            AddRelationship("Rivendell Blade", "RtR", "Foe-hammer", "THOHaUH");
            AddRelationship("Rivendell Blade", "RtR", "Goblin-cleaver", "THOHaUH");

            AddRelationship("Aragorn", "TWitW", "Celebrian's Stone", "Core");
            AddRelationship("Aragorn", "TWitW", "Protector of Lorien", "Core");
            AddRelationship("Aragorn", "TWitW", "Rivendell Minstrel", "THfG");
            AddRelationship("Aragorn", "TWitW", "Song of Kings", "THfG");
            AddRelationship("Aragorn", "TWitW", "A Burning Brand", "CatC");
            AddRelationship("Aragorn", "TWitW", "Song of Travel", "THoEM");
            AddRelationship("Aragorn", "TWitW", "Sword that was Broken", "TWitW");
            AddRelationship("Aragorn", "TWitW", "Ring of Barahir", "TSF");

            AddRelationship("Arwen Undomiel", "TWitW", "Gandalf", "THOHaUH");
            AddRelationship("Arwen Undomiel", "TWitW", "Elrond's Counsel", "TWitW");
            
            AddRelationship("Elrond's Counsel", "TWitW", "A Test of Will", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Gandalf", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Gandalf", "THOHaUH");
            AddRelationship("Elrond's Counsel", "TWitW", "Arwen Undomiel", "TWitW");
            AddRelationship("Elrond's Counsel", "TWitW", "Glorfindel", "FoS");
            
            AddRelationship("Legacy of Durin", "TWitW", "Miner of the Iron Hills", "Core");
            AddRelationship("Legacy of Durin", "TWitW", "Erebor Record Keeper", "KD");
            AddRelationship("Legacy of Durin", "TWitW", "Lure of Moria", "RtR");
            AddRelationship("Legacy of Durin", "TWitW", "Daeron's Runes", "FoS");
            AddRelationship("Legacy of Durin", "TWitW", "We Are Not Idle", "SaF");
            AddRelationship("Legacy of Durin", "TWitW", "Erebor Hammersmith", "Core");
            AddRelationship("Legacy of Durin", "TWitW", "Longbeard Map-Maker", "CatC");
            AddRelationship("Legacy of Durin", "TWitW", "Bifur", "KD");
            AddRelationship("Legacy of Durin", "TWitW", "Ori", "THOHaUH");
            AddRelationship("Legacy of Durin", "TWitW", "Bombur", "THOtD");
            
            AddRelationship("Resourceful", "TWitW", "Arwen Undomiel", "TWitW");
            AddRelationship("Resourceful", "TWitW", "Glorfindel", "FoS");
            
            AddRelationship("Sword that was Broken", "TWitW", "Steward of Gondor", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Celebrian's Stone", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Aragorn", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Faramir", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Visionary Leadership", "TMV");

            AddRelationship("Erebor Battle Master", "TLD", "Gimli", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "Thalin", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "Bofur", "THOHaUH");
            AddRelationship("Erebor Battle Master", "TLD", "Veteran Axehand", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "Veteran of Nanduhirion", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "We Are Not Idle", "SaF");
            
            AddRelationship("Warden of Healing", "TLD", "Elrond", "SaF");
            AddRelationship("Warden of Healing", "TLD", "Daughter of the Nimrodel", "Core");
            AddRelationship("Warden of Healing", "TLD", "Denethor", "Core");
            AddRelationship("Warden of Healing", "TLD", "Faramir", "AoO");

            AddRelationship("Asfaloth", "FoS", "Glorfindel", "Core");
            AddRelationship("Asfaloth", "FoS", "Glorfindel", "FoS");
            AddRelationship("Asfaloth", "FoS", "Elf-Stone", "TBR");
            AddRelationship("Asfaloth", "FoS", "Ancient Mathom", "AJtR");
            AddRelationship("Asfaloth", "FoS", "Northern Tracker", "Core");

            AddRelationship("Glorfindel", "FoS", "Glorfindel", "Core");
            AddRelationship("Glorfindel", "FoS", "Unexpected Courage", "Core");
            AddRelationship("Glorfindel", "FoS", "Arwen Undomiel", "TWitW");
            AddRelationship("Glorfindel", "FoS", "Imladris Stargazer", "FoS");
            AddRelationship("Glorfindel", "FoS", "Light of Valinor", "FoS");
            AddRelationship("Glorfindel", "FoS", "Gandalf", "Core");
            AddRelationship("Glorfindel", "FoS", "Gandalf", "THOHaUH");
            AddRelationship("Glorfindel", "FoS", "Elrond's Counsel", "TWitW");
            AddRelationship("Glorfindel", "FoS", "Asfaloth", "FoS");

            AddRelationship("Imladris Stargazer", "FoS", "Elrond", "SaF");
            AddRelationship("Imladris Stargazer", "FoS", "Vilya", "SaF");
            AddRelationship("Imladris Stargazer", "FoS", "Zigil Miner", "KD");
            
            AddRelationship("Light of Valinor", "FoS", "Glorfindel", "FoS");
            AddRelationship("Light of Valinor", "FoS", "Glorfindel", "Core");
            AddRelationship("Light of Valinor", "FoS", "Elrond", "SaF");
            AddRelationship("Light of Valinor", "FoS", "Elladan", "RtR");
            AddRelationship("Light of Valinor", "FoS", "Elrohir", "TRG");
            
            AddRelationship("Longbeard Elder", "FoS", "Longbeard Orc Slayer", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Lure of Moria", "RtR");
            AddRelationship("Longbeard Elder", "FoS", "We Are Not Idle", "SaF");
            AddRelationship("Longbeard Elder", "FoS", "Fili", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Kili", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Gloin", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Brok Ironfist", "Core");

            AddRelationship("Elrond", "SaF", "Gildor Inglorion", "THoEM");
            AddRelationship("Elrond", "SaF", "A Burning Brand", "CatC");
            AddRelationship("Elrond", "SaF", "Vilya", "SaF");
            AddRelationship("Elrond", "SaF", "Warden of Healing", "TLD");
            AddRelationship("Elrond", "SaF", "Imladris Stargazer", "FoS");
            AddRelationship("Elrond", "SaF", "Unexpected Courage", "Core");
            AddRelationship("Elrond", "SaF", "Miruvor", "SaF");

            AddRelationship("Hands Upon the Bow", "SaF", "Legolas", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Vassal of the Windlord", "TDM");
            AddRelationship("Hands Upon the Bow", "SaF", "Brand son of Bain", "THoEM");
            AddRelationship("Hands Upon the Bow", "SaF", "Bard the Bowman", "THOtD");
            AddRelationship("Hands Upon the Bow", "SaF", "Horseback Archer", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Trollshaw Scout", "FoS");

            AddRelationship("Master of the Forge", "SaF", "Gleowine", "Core");
            AddRelationship("Master of the Forge", "SaF", "Imladris Stargazer", "FoS");
            AddRelationship("Master of the Forge", "SaF", "Gildor Inglorion", "THoEM");
            
            AddRelationship("Miruvor", "SaF", "Unexpected Courage", "Core");
            
            AddRelationship("Peace, and Thought", "SaF", "Gleowine", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Unexpected Courage", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Fast Hitch", "TDM");
            AddRelationship("Peace, and Thought", "SaF", "Cram", "THOHaUH");
            AddRelationship("Peace, and Thought", "SaF", "Miruvor", "SaF");

            AddRelationship("We Are Not Idle", "SaF", "Lure of Moria", "RtR");
            AddRelationship("We Are Not Idle", "SaF", "Longbeard Elder", "FoS");
            AddRelationship("We Are Not Idle", "SaF", "Cram", "THOHaUH");
            
            AddRelationship("Envoy of Pelargir", "HoN", "White Tower Watchman", "TDF");
            
            AddRelationship("Errand-rider", "HoN", "Faramir", "Core");
            AddRelationship("Errand-rider", "HoN", "Steward of Gondor", "Core");
            AddRelationship("Errand-rider", "HoN", "Boromir", "HoN");
            
            AddRelationship("Ithilien Tracker", "HoN", "Denethor", "Core");
            AddRelationship("Ithilien Tracker", "HoN", "Warden of Healing", "TLD");
            AddRelationship("Ithilien Tracker", "HoN", "Ranger Spikes", "HoN");
            AddRelationship("Ithilien Tracker", "HoN", "Anborn", "TBoG");
            AddRelationship("Ithilien Tracker", "HoN", "Ithilien Archer", "EaAD");
            AddRelationship("Ithilien Tracker", "HoN", "Ranger Bow", "AoO");
            
            AddRelationship("Ranger Spikes", "HoN", "Erebor Hammersmith", "Core");
            AddRelationship("Ranger Spikes", "HoN", "Anborn", "TBoG");
            AddRelationship("Ranger Spikes", "HoN", "Poisoned Stakes", "TBoG");
            AddRelationship("Ranger Spikes", "HoN", "Ithilien Pit", "EaAD");

            AddRelationship("Gondorian Shield", "TSF", "Denethor", "Core");
            AddRelationship("Gondorian Shield", "TSF", "Boromir", "TDM");
            AddRelationship("Gondorian Shield", "TSF", "Beregond", "HoN");
            AddRelationship("Gondorian Shield", "TSF", "Horn of Gondor", "Core");
            
            AddRelationship("A Very Good Tale", "THOHaUH", "Sneak Attack", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Steward of Gondor", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Gandalf", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Gandalf", "THOHaUH");
            AddRelationship("A Very Good Tale", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("A Very Good Tale", "THOHaUH", "Fili", "THOHaUH");
            AddRelationship("A Very Good Tale", "THOHaUH", "Kili", "THOHaUH");
            
            AddRelationship("Fili", "THOHaUH", "Zigil Miner", "KD");
            AddRelationship("Fili", "THOHaUH", "Bofur", "TRG");
            AddRelationship("Fili", "THOHaUH", "Lure of Moria", "RtR");
            AddRelationship("Fili", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Fili", "THOHaUH", "A Very Good Tale", "THOHaUH");
            AddRelationship("Fili", "THOHaUH", "Kili", "THOHaUH");
            AddRelationship("Fili", "THOHaUH", "Balin", "THOtD");
            AddRelationship("Fili", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("Fili", "THOHaUH", "Longbeard Elder", "FoS");
            AddRelationship("Fili", "THOHaUH", "Cram", "THOHaUH");

            AddRelationship("Foe-hammer", "THOHaUH", "Gondorian Spearman", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Feint", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Gandalf", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Hands Upon the Bow", "SaF");
            AddRelationship("Foe-hammer", "THOHaUH", "Bofur", "THOHaUH");

            AddRelationship("Gandalf", "THOHaUH", "Glorfindel", "FoS");
            AddRelationship("Gandalf", "THOHaUH", "Elrond's Counsel", "TWitW");
            AddRelationship("Gandalf", "THOHaUH", "The Galadhrim's Greeting", "Core");

            AddRelationship("Kili", "THOHaUH", "Erebor Record Keeper", "KD");
            AddRelationship("Kili", "THOHaUH", "Narvi's Belt", "KD");
            AddRelationship("Kili", "THOHaUH", "Zigil Miner", "KD");
            AddRelationship("Kili", "THOHaUH", "Bofur", "TRG");
            AddRelationship("Kili", "THOHaUH", "Lure of Moria", "RtR");
            AddRelationship("Kili", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Kili", "THOHaUH", "A Very Good Tale", "THOHaUH");
            AddRelationship("Kili", "THOHaUH", "Fili", "THOHaUH");
            AddRelationship("Kili", "THOHaUH", "Balin", "THOtD");
            AddRelationship("Kili", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("Kili", "THOHaUH", "Longbeard Elder", "FoS");

            AddRelationship("Balin", "THOtD", "Steward of Gondor", "Core");
            AddRelationship("Balin", "THOtD", "Gandalf", "Core");
            AddRelationship("Balin", "THOtD", "We Are Not Idle", "SaF");
            AddRelationship("Balin", "THOtD", "Errand-rider", "HoN");
            AddRelationship("Balin", "THOtD", "Gaining Strength", "TSF");
            AddRelationship("Balin", "THOtD", "A Very Good Tale", "THOHaUH");
            AddRelationship("Balin", "THOtD", "King Under the Mountain", "THOtD");
            AddRelationship("Balin", "THOtD", "Narvi's Belt", "KD");
            AddRelationship("Balin", "THOtD", "Lure of Moria", "RtR");
            AddRelationship("Balin", "THOtD", "Fili", "THOHaUH");
            AddRelationship("Balin", "THOtD", "Kili", "THOHaUH");
            
            AddRelationship("King Under the Mountain", "THOtD", "Steward of Gondor", "Core");
            AddRelationship("King Under the Mountain", "THOtD", "We Are Not Idle", "SaF");
            AddRelationship("King Under the Mountain", "THOtD", "A Very Good Tale", "THOHaUH");
            AddRelationship("King Under the Mountain", "THOtD", "Balin", "THOtD");
            AddRelationship("King Under the Mountain", "THOtD", "Narvi's Belt", "KD");
            AddRelationship("King Under the Mountain", "THOtD", "Lure of Moria", "RtR");
            AddRelationship("King Under the Mountain", "THOtD", "Fili", "THOHaUH");
            AddRelationship("King Under the Mountain", "THOtD", "Kili", "THOHaUH");

            AddRelationship("Boromir", "TDM", "Boromir", "HoN");
            AddRelationship("Boromir", "TDM", "Gandalf", "Core");

            AddRelationship("Pippin", "EaAD", "Pippin", "TBR");

            AddRelationship("Eomer", "VoI", "Theoden", "TMV");
            AddRelationship("Eomer", "VoI", "Hama", "TLD");
            AddRelationship("Eomer", "VoI", "Eomund", "CatC");

            AddRelationship("Legacy of Numenor", "VoI", "Deep Knowledge", "VoI");
            AddRelationship("Legacy of Numenor", "VoI", "Power of Orthanc", "VoI");
            AddRelationship("Legacy of Numenor", "VoI", "The Wizard's Voice", "VoI");
            AddRelationship("Legacy of Numenor", "VoI", "The Seeing-stone", "VoI");
            AddRelationship("Deep Knowledge", "VoI", "Power of Orthanc", "VoI");
            AddRelationship("Deep Knowledge", "VoI", "The Wizard's Voice", "VoI");
            AddRelationship("Deep Knowledge", "VoI", "The Seeing-stone", "VoI");
            AddRelationship("Power of Orthanc", "VoI", "The Wizard's Voice", "VoI");
            AddRelationship("Power of Orthanc", "VoI", "The Seeing-Stone", "VoI");
            AddRelationship("The Wizard's Voice", "VoI", "The Seeing-Stone", "VoI");

            AddRelationship("Westfold Horse-breeder", "VoI", "Steed of the Mark", "TMV");
            AddRelationship("Westfold Horse-breeder", "VoI", "Rohan Warhorse", "VoI");
            AddRelationship("Westfold Horse-breeder", "VoI", "Asfaloth", "FoS");
        }

        public IEnumerable<Card> All()
        {
            return cards.Values.ToList();
        }

        public IEnumerable<Card> Search(SearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Query) && model.Query.Trim().ToLower() == randomKeyword)
            {
                model.Random = true;
            }

            var results = !string.IsNullOrEmpty(model.Query) ?
                cards.Values.Where(
                    x => (x.Title.ToLower().Contains(model.Query.ToLower()) || model.Query.ToLower() == randomKeyword)
                    || (!string.IsNullOrEmpty(x.NormalizedTitle) && x.NormalizedTitle.ToLower().Contains(model.Query.ToLower()))
                    || (!string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(model.Query.ToLower()))
                    || x.Keywords.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.NormalizedKeywords.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.Traits.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    || x.NormalizedTraits.Any(y => y != null && y.ToLower().Contains(model.Query.ToLower()))
                    )
                .ToList()
                : cards.Values.ToList();

            if (model.CardType != CardType.None)
            {
                if (model.CardType == CardType.Player)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event).ToList();
                }
                else if (model.CardType == CardType.Character)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Objective_Ally || (x.CardType == CardType.Objective && x.HitPoints > 0)).ToList();
                }
                else if (model.CardType == CardType.Encounter)
                {
                    results = results.Where(x => x.CardType == CardType.Enemy || x.CardType == CardType.Location || x.CardType == CardType.Treachery || x.CardType == CardType.Objective || x.CardType == CardType.Objective_Ally).ToList();
                }
                else if (model.CardType == CardType.Objective)
                {
                    results = results.Where(x => x.CardType == CardType.Objective || x.CardType == CardType.Objective_Ally).ToList();
                }
                else if (model.CardType == CardType.Boon)
                {
                    results = results.Where(x => x.CampaignCardType == CampaignCardType.Boon).ToList();
                }
                else if (model.CardType == CardType.Burden)
                {
                    results = results.Where(x => x.CampaignCardType == CampaignCardType.Burden).ToList();
                }
                else
                    results = results.Where(x => x.CardType == model.CardType).ToList();
            }

            if (model.CardSet != null && model.CardSet != "Any")
            {
                results = results.Where(x => x.CardSet.Name == model.CardSet || (!string.IsNullOrEmpty(x.CardSet.Cycle) && x.CardSet.Cycle.ToUpper() == model.CardSet)).ToList();
            }

            if (model.Trait != null && model.Trait != "Any")
            {
                results = results.Where(x => x.HasTrait(model.Trait)).ToList();
            }

            if (model.Keyword != null && model.Keyword != "Any")
            {
                results = results.Where(x => x.HasKeyword(model.Keyword)).ToList();
            }

            if (model.Sphere != Sphere.None)
            {
                results = results.Where(x => x.Sphere == model.Sphere).ToList();
            }

            if (model.Cost != -1)
            {
                results = results.Where(x => x.ResourceCost == model.Cost).ToList();
            }

            if (model.Unique)
            {
                results = results.Where(x => x.IsUnique).ToList();
            }

            if (!string.IsNullOrEmpty(model.Artist) && model.Artist != "Any")
            {
                results = results.Where(x => x.Artist != null && x.Artist.Name == model.Artist).ToList();
            }

            var takeCount = model.Random || !string.IsNullOrEmpty(model.Artist) ? results.Count : maxResults;

            results = results.Take(takeCount).ToList();

            if (model.Random)
            {
                var total = results.Count();
                if (total > 1)
                {
                    var random = new Random();
                    var choice = random.Next(0, total - 1);
                    results = new List<Card> { results[choice] };
                }

                return results;
            }
            else
            {
                switch (model.Sort)
                {
                    case Sort.Alphabetical:
                        return results.OrderBy(x => x.Title);
                    case Sort.Sphere_Type_Cost:
                        return results.OrderBy(x => x.Sphere).ThenBy(x => x.CardType).ThenBy(x => 
                            {
                                if (x.ThreatCost > 0)
                                    return x.ThreatCost;
                                else if (x.ResourceCost > 0)
                                    return x.ResourceCost;
                                else if (x.EngagementCost > 0)
                                    return x.EngagementCost;
                                else if (x.QuestPoints.HasValue && x.QuestPoints > 0)
                                    return x.QuestPoints.Value;
                                else
                                    return -1;
                            });
                    case Sort.Set_Number:
                    default:
                        return results.OrderBy(x => x.CardSet.Number).ThenBy(x => x.Number);
                }
            }
        }

        public Card Find(string id)
        {
            return cards.ContainsKey(id) ?
                cards[id]
                : null;
        }

        public Card FindBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            return cards.Values.Where(x => x.Slug.ToLower() == slug.ToLower()).FirstOrDefault();
        }

        public IEnumerable<byte> Costs()
        {
            return cards.Values.Select(x => x.ResourceCost).Distinct().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> SetNames
        {
            get { return setNames; }
        }

        public IEnumerable<string> Keywords()
        {
            return keywords.Values.ToList().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> Traits()
        {
            return traits.Values.ToList().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> Spheres()
        {
            foreach (var item in SearchViewModel.Spheres)
                yield return item.Text;
        }
    }
}