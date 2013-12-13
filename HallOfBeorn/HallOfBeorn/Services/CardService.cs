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
        }

        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly List<string> setNames = new List<string>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();
        private readonly Dictionary<string, string> keywords = new Dictionary<string, string>();
        private readonly Dictionary<string, string> traits = new Dictionary<string, string>();
        private readonly List<Deck> decks = new List<Deck>();

        const int maxResults = 128;

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
                    if (!keywords.ContainsKey(keywordKey))
                        keywords.Add(keywordKey, keywordKey);
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

            AddSet(new TheHobbitOverHillandUnderHill());
            AddSet(new TheHobbitOntheDoorstep());
            AddSet(new TheBlackRiders());

            AddSet(new TheMassingatOsgiliath());
            AddSet(new TheBattleofLakeTown());
            AddSet(new TheStoneofErech());

            //AddSet(new IsengardCyclePlayerCards());
            //AddSet(new Ninineliph());
            //AddSet(new TheDunlandTrap());
            //AddSet(new TheThreeTrials());
            //AddSet(new TroubleinTharbad());
            //AddSet(new BoarandRaven());
            //AddSet(new CelebrimborsForge());
            //AddSet(new TheRoadDarkens());
        }

        private void LoadDecks()
        {
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

        public IEnumerable<Card> All()
        {
            return cards.Values.ToList().Take(maxResults);
        }

        public IEnumerable<Card> Search(SearchViewModel model)
        {
            var results = !string.IsNullOrEmpty(model.Query) ?
                cards.Values.Where(
                    x => x.Title.ToLower().Contains(model.Query.ToLower())
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

            results = results.Take(maxResults).ToList();

            switch (model.Sort)
            {
                case Sort.Set_and_number:
                    return results.OrderBy(x => x.CardSet.Number).ThenBy(x => x.Number);
                case Sort.Alphabetical:
                    return results.OrderBy(x => x.Title);
                case Sort.Sphere_type_cost:
                    return results.OrderBy(x => x.Sphere).ThenBy(x => x.CardType).ThenBy(x => x.ResourceCost > 0 ? x.ResourceCost : x.ThreatCost);
                default:
                    return results;
            }
        }

        public Card Find(string id)
        {
            return cards.ContainsKey(id) ?
                cards[id]
                : null;
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
    }
}