﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallOfBeorn.Models
{
    public class FilterViewModel
    {
        [Display(Name="Search")]
        public string Query { get; set; }

        [Display(Name="Type")]
        public CardType CardType { get; set; }

        [Display(Name = "Set")]
        public string CardSet { get; set; }
    }

    public class SearchViewModel
    {
        public SearchViewModel()
        {
            //AttackStrengthValues = new List<SelectListItem>() { new SelectListItem() { Text = "Foo", Value = "Foo" } };
            //DefenseStrengthValues = new List<SelectListItem>() { new SelectListItem() { Text = "Foo", Value = "Foo" } };
            //HitPointsValues = new List<SelectListItem>() { new SelectListItem() { Text = "Foo", Value = "Foo" } };
        }

        public const string DEFAULT_FILTER_VALUE = "Any";
        public const string RANDOM_KEYWORD = "+random";

        [Display(Name = "Search")]
        public string Query { get; set; }

        [Display(Name = "Type")]
        public CardType CardType { get; set; }

        [Display(Name = "Subtype")]
        public CardSubtype CardSubtype { get; set; }

        [Display(Name="Deck Type")]
        public DeckType DeckType { get; set; }

        [Display(Name = "Set")]
        public string CardSet { get; set; }

        [Display(Name="Scenario")]
        public string Scenario { get; set; }

        [Display(Name = "Trait")]
        public string Trait { get; set; }

        [Display(Name = "Keyword")]
        public string Keyword { get; set; }

        [Display(Name = "Cost")]
        public string Cost { get; set; }

        [Display(Name = "Cost Operator")]
        public NumericOperator CostOperator { get; set; }

        [Display(Name = "Threat Cost")]
        public string ThreatCost { get; set; }

        [Display(Name = "Threat Cost Operator")]
        public NumericOperator ThreatCostOperator { get; set; }

        [Display(Name = "Engagement Cost")]
        public string EngagementCost { get; set; }

        [Display(Name = "Engagement Cost Operator")]
        public NumericOperator EngagementCostOperator { get; set; }

        [Display(Name = "Attack Strength")]
        public string Attack { get; set; }

        [Display(Name = "Attack Strength Operator")]
        public NumericOperator AttackOp { get; set; }

        [Display(Name = "Defense Strength")]
        public string Defense { get; set; }

        [Display(Name = "Defense Strength Operator")]
        public NumericOperator DefenseOp { get; set; }

        [Display(Name = "Hit Points")]
        public string HitPoints { get; set; }

        [Display(Name = "Hit Points Operator")]
        public NumericOperator HitPointsOp { get; set; }

        [Display(Name = "Sphere")]
        public Sphere Sphere { get; set; }

        [Display(Name = "Unique")]
        public bool Unique { get; set; }

        [Display(Name = "Unique")]
        public Uniqueness IsUnique { get; set; }

        [Display(Name = "Set Type")]
        public SetType SetType { get; set; }

        [Display(Name = "Sort")]
        public Sort Sort { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name="Encounter Set")]
        public string EncounterSet { get; set; }

        [Display(Name="Player Category")]
        public string Category { get; set; }

        public Category GetCategory()
        {
            var category = HallOfBeorn.Models.Category.None;
            if (string.IsNullOrEmpty(this.Category))
                return category;

            var decoded = HttpUtility.HtmlDecode(this.Category).Replace(' ', '_');

            Enum.TryParse<HallOfBeorn.Models.Category>(decoded, true, out category);
            
            return category;
        }

        [Display(Name="Encounter Category")]
        public string EncounterCategory { get; set; }

        public EncounterCategory GetEncounterCategory()
        {
            var encCategory = HallOfBeorn.Models.EncounterCategory.None;
            if (string.IsNullOrEmpty(this.EncounterCategory))
                return encCategory;

            var decoded = HttpUtility.HtmlDecode(this.EncounterCategory).Replace(' ', '_');

            Enum.TryParse<HallOfBeorn.Models.EncounterCategory>(decoded, true, out encCategory);

            return encCategory;
        }

        [Display(Name = "Quest Category")]
        public string QuestCategory { get; set; }

        public QuestCategory GetQuestCategory()
        {
            var questCategory = HallOfBeorn.Models.QuestCategory.None;
            if (string.IsNullOrEmpty(this.QuestCategory))
                return questCategory;

            var decoded = HttpUtility.HtmlDecode(this.QuestCategory).Replace(' ', '_');

            Enum.TryParse<HallOfBeorn.Models.QuestCategory>(decoded, true, out questCategory);

            return questCategory;
        }

        public bool IsRandom()
        {
            return (!string.IsNullOrEmpty(this.Query) && this.Query.ContainsLower(RANDOM_KEYWORD));
        }

        [Display(Name = "Results")]
        public List<CardViewModel> Cards { get; set; }

        [Display(Name = "Custom")]
        public bool Custom { get; set; }

        [Display(Name = "Victory")]
        public string VictoryPoints { get; set; }

        public HasShadow HasShadow { get; set; }

        public string Quest { get; set; }

        public bool HasQuest()
        {
            return !string.IsNullOrEmpty(this.Quest);
        }

        public bool HasQuery()
        {
            return !string.IsNullOrEmpty(this.Query);
        }

        public bool HasCardType()
        {
            return this.CardType != Models.CardType.None;
        }

        public bool HasCardSubtype()
        {
            return this.CardSubtype != Models.CardSubtype.None;
        }

        public bool HasDeckType()
        {
            return this.DeckType != Models.DeckType.None;
        }

        public bool HasCardSet()
        {
            return !string.IsNullOrEmpty(this.CardSet) && this.CardSet != DEFAULT_FILTER_VALUE;
        }

        //public bool HasSetType()
        //{
        //    return !string.IsNullOrEmpty(this.SetType
        //}

        public bool HasScenario()
        {
            return !string.IsNullOrEmpty(this.Scenario) && this.Scenario != DEFAULT_FILTER_VALUE;
        }

        public bool HasTrait()
        {
            return !string.IsNullOrEmpty(this.Trait) && this.Trait != DEFAULT_FILTER_VALUE;
        }

        public bool HasKeyword()
        {
            return !string.IsNullOrEmpty(this.Keyword) && this.Keyword != DEFAULT_FILTER_VALUE;
        }

        public bool HasSphere()
        {
            return this.Sphere != Models.Sphere.None;
        }

        public bool HasCategory()
        {
            return this.GetCategory() != Models.Category.None;
        }

        public bool HasEncounterCategory()
        {
            return this.GetEncounterCategory() != Models.EncounterCategory.None;
        }

        public bool HasQuestCategory()
        {
            return this.GetQuestCategory() != Models.QuestCategory.None;
        }

        public bool HasResourceCost()
        {
            return !string.IsNullOrEmpty(this.Cost) && this.Cost != DEFAULT_FILTER_VALUE;
        }

        public bool HasThreatCost()
        {
            return !string.IsNullOrEmpty(this.ThreatCost) && this.ThreatCost != DEFAULT_FILTER_VALUE;
        }

        public bool HasEngagementCost()
        {
            return !string.IsNullOrEmpty(this.EngagementCost) && this.EngagementCost != DEFAULT_FILTER_VALUE;
        }

        public bool HasHitPoints()
        {
            return !string.IsNullOrEmpty(this.HitPoints) && this.HitPoints != DEFAULT_FILTER_VALUE;
        }

        public bool HasArtist()
        {
            return !string.IsNullOrEmpty(this.Artist) && this.Artist != DEFAULT_FILTER_VALUE;
        }

        public bool HasEncounterSet()
        {
            return !string.IsNullOrEmpty(this.EncounterSet) && this.EncounterSet != DEFAULT_FILTER_VALUE;
        }

        public bool HasVictoryPoints()
        {
            return !string.IsNullOrEmpty(this.VictoryPoints) && this.VictoryPoints != DEFAULT_FILTER_VALUE;
        }

        public bool HasFilter()
        {
            if (HasQuery())
                return true;

            if (HasCardType())
                return true;

            if (HasCardSet())
                return true;

            if (HasTrait())
                return true;

            if (HasKeyword())
                return true;

            if (HasSphere())
                return true;

            if (HasCategory())
                return true;

            if (HasResourceCost())
                return true;

            if (this.Unique)
                return true;

            if (this.IsUnique != Uniqueness.Any)
                return true;

            if (HasArtist())
                return true;

            if (HasEncounterSet())
                return true;

            if (HasVictoryPoints())
                return true;

            return false;
        }

        public bool CardTypeMatches(Card card)
        {
            if (CardType == CardType.Player)
            {
                return card.CardType == CardType.Hero || card.CardType == Models.CardType.Ally || card.CardType == Models.CardType.Attachment || card.CardType == Models.CardType.Event;
            }
            else if (CardType == CardType.Character)
            {
                return card.CardType == Models.CardType.Hero || card.CardType == Models.CardType.Ally || card.CardType == Models.CardType.Objective_Ally || (card.CardType == Models.CardType.Objective && card.HitPoints > 0);
            }
            else if (CardType == CardType.Encounter)
            {
                return card.CardType == Models.CardType.Enemy || card.CardType == Models.CardType.Location || card.CardType == Models.CardType.Treachery || card.CardType == Models.CardType.Objective || card.CardType == Models.CardType.Objective_Ally;
            }
            else if (CardType == CardType.Objective)
            {
                return card.CardType == Models.CardType.Objective || card.CardType == Models.CardType.Objective_Ally;
            }
            else
                return CardType == card.CardType;
        }

        public bool CardSetMatches(Card card)
        {
            return card.CardSet.Name == this.CardSet || (!string.IsNullOrEmpty(card.CardSet.AlternateName) && card.CardSet.AlternateName == this.CardSet) || (!string.IsNullOrEmpty(card.CardSet.NormalizedName) && card.CardSet.NormalizedName == this.CardSet) || (!string.IsNullOrEmpty(card.CardSet.Cycle) && card.CardSet.Cycle.ToUpper() == this.CardSet);
        }

        public bool CardIsCustom(Card card)
        {
            if ((this.CardSet == null || this.CardSet == "Any") && (this.EncounterSet == null || this.EncounterSet == "Any") && this.Sphere != Sphere.Mastery && (this.Trait == null || this.Trait == "Any") && (this.Keyword == null || this.Keyword == "Any"))
            {
                return card.CardSet.SetType == SetType.CUSTOM;
            }

            return false;
        }

        public bool VictoryPointsMatch(Card card)
        {
            if (!HasVictoryPoints())
                return false;

            byte victoryPoints = 0;
            if (byte.TryParse(this.VictoryPoints.Replace("Victory", string.Empty).Trim('.'), out victoryPoints))
            {
                return card.VictoryPoints == victoryPoints;
            }

            return false;
        }

        public bool IsAdvancedSearch()
        {
            if (string.IsNullOrEmpty(this.Query))
                return false;

            return (this.Query.StartsWith("-") || this.Query.StartsWith("+") || this.Query.Contains(" -") || this.Query.Contains(" +"));
        }

        private string basicQuery;

        public string BasicQuery()
        {
            if (basicQuery == null)
            {
                if (IsAdvancedSearch())
                {
                    var parts = this.Query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToListSafe().Where(x => !x.StartsWith("-") && !x.StartsWith("+")).ToListSafe();

                    if (parts.Count == 0)
                        return string.Empty;

                    basicQuery = string.Join(" ", parts).ToLowerSafe();
                }
                else
                {
                    basicQuery = this.Query.ToLowerSafe();
                }
            }

            return basicQuery;
        }

        public static IEnumerable<SelectListItem> CardTypes
        {
            get { return typeof(CardType).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> CardSubtypes
        {
            get { return typeof(CardSubtype).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> DeckTypes
        {
            get { return typeof(DeckType).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> GameTypes { get; set; }

        public static IEnumerable<SelectListItem> Keywords
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Traits
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Categories
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> EncounterCategories
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> QuestCategories
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> CardSets { get; set; }
        public static IEnumerable<SelectListItem> Scenarios { get; set; }
        public static IEnumerable<SelectListItem> EncounterSets { get; set; }

        public static IEnumerable<SelectListItem> ResourceCosts { get; set; }
        public static IEnumerable<SelectListItem> ThreatCosts { get; set; }
        public static IEnumerable<SelectListItem> EngagementCosts { get; set; }

        public static IEnumerable<SelectListItem> AttackStrengthValues { get; set; }
        public static IEnumerable<SelectListItem> DefenseStrengthValues { get; set; }
        public static IEnumerable<SelectListItem> HitPointsValues { get; set; }
        public static IEnumerable<SelectListItem> WillpowerStrengthValues { get; set; }
        public static IEnumerable<SelectListItem> ThreatStrengthValues { get; set; }
        public static IEnumerable<SelectListItem> QuestPointsValues { get; set; }

        public static IEnumerable<SelectListItem> Spheres
        {
            get { return typeof(Sphere).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> Sorts
        {
            get { return typeof(Sort).GetSelectListItems(", "); }
        }

        public static IEnumerable<SelectListItem> UniquenessValues
        {
            get { return typeof(Uniqueness).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> SetTypeValues
        {
            get { return typeof(SetType).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> NumericOperators
        {
            get {
                Func<NumericOperator, string> mapFunction = (n) =>
                {
                    switch (n) {
                        case NumericOperator.eq:
                        default:
                            return "=";
                        case NumericOperator.gt:
                            return ">";
                        case NumericOperator.gteq:
                            return ">=";
                        case NumericOperator.lt:
                            return "<";
                        case NumericOperator.lteq:
                            return "<=";
                    }
                };
                return typeof(NumericOperator).GetSelectListItems<NumericOperator>(mapFunction); 
            }
        }

        public static IEnumerable<SelectListItem> Artists
        {
            get
            {
                yield return new SelectListItem { Text = "Any", Value = "Any" };

                foreach (var artist in HallOfBeorn.Models.Artist.All())
                    yield return new SelectListItem { Text = artist.Name, Value = artist.Name };
            }
        }

        public static IEnumerable<SelectListItem> VictoryPointValues { get; set; }
    }
}