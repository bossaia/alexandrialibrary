﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Simple
{
    public class Side
    {
        public Side()
        {
            Stats = new Dictionary<string, string>();
            Traits = new List<string>();
            Keywords = new List<string>();
            Text = new List<string>();
        }

        public string Subtitle { get; set; }
        public string ImagePath { get; set; }

        public Dictionary<string, string> Stats { get; private set; }
        public List<string> Traits { get; private set; }
        public List<string> Keywords { get; private set; }
        public List<string> Text { get; private set; }
        public string FlavorText { get; set; }
    }

    public class SimpleCard
    {
        public SimpleCard(Card card)
            : this()
        {
            this.Front = new Side() { ImagePath = GetFrontImagePath(card) };

            InitializeText(card);

            this.Title = card.Title;
            this.IsUnique = card.IsUnique;
            this.CardType = card.CardType.ToString();
            this.CardSubType = card.CardSubtype.ToString();
            this.Sphere = (card.Sphere != Models.Sphere.None && card.Sphere != Models.Sphere.Neutral) ? card.Sphere.ToString() : null;
            this.CardSet = card.CardSet.Name;
            this.Number = card.Number;
            this.Quantity = card.Quantity;
            this.Artist = card.Artist != null ? card.Artist.Name : null;
            this.HasErrata = card.HasErrata;

            foreach (var trait in card.Traits)
            {
                this.Front.Traits.Add(trait.Trim());
            }

            foreach (var keyword in card.Keywords)
            {
                this.Front.Keywords.Add(keyword.Replace("~", string.Empty).Replace("[Card]", card.Title).Trim());
            }

            if (card.PassValue.HasValue && card.PassValue.Value)
            {
                this.Front.Keywords.Add("PASS.");
            }

            if (card.VictoryPoints > 0)
            {
                this.Front.Keywords.Add("Victory " + card.VictoryPoints.ToString() + ".");
            }

            switch (card.CardType)
            {
                case Models.CardType.Hero:
                    InitializeHero(card);
                    break;
                case Models.CardType.Ally:
                    InitializeAlly(card);
                    break;
                case Models.CardType.Attachment:
                    InitializeEvent(card);
                    break;
                case Models.CardType.Event:
                    InitializeEvent(card);
                    break;
                case Models.CardType.Treasure:
                    InitializeBoon(card);
                    break;
                case Models.CardType.Quest:
                    InitializeEncounterSet(card);
                    InitializeQuest(card);
                    break;
                case Models.CardType.Enemy:
                    InitializeEncounterSet(card);
                    InitializeEnemy(card);
                    break;
                case Models.CardType.Location:
                    InitializeEncounterSet(card);
                    InitializeLocation(card);
                    break;
                case Models.CardType.Treachery:
                    InitializeEncounterSet(card);
                    break;
                case Models.CardType.Objective:
                case Models.CardType.Objective_Ally:
                    InitializeEncounterSet(card);
                    InitializeObjective(card);
                    break;
                default:
                    break;
            }
        }

        public SimpleCard()
        {
        }

        public string Title { get; set; }
        public bool IsUnique { get; set; }
        public string CardType { get; set; }
        public string CardSubType { get; set; }
        public string Sphere { get; set; }
        public Side Front { get; set; }
        public Side Back { get; set; }
        public string CardSet { get; set; }
        public SimpleEncounterInfo EncounterInfo { get; set; }

        public uint Number { get; set; }
        public uint Quantity { get; set; }
        public string Artist { get; set; }
        public bool HasErrata { get; set; }

        #region Constants
        public const string STAT_SPHERE = "Sphere";
        public const string STAT_THREAT_COST = "ThreatCost";
        public const string STAT_RESOURCE_COST = "ResourceCost";
        public const string STAT_ENGAGEMENT_COST = "EngagementCost";
        public const string STAT_WILLPOWER = "Willpower";
        public const string STAT_THREAT = "Threat";
        public const string STAT_ATTACK = "Attack";
        public const string STAT_DEFENSE = "Defense";
        public const string STAT_HIT_POINTS = "HitPoints";
        public const string STAT_QUEST_POINTS = "QuestPoints";
        public const string STAT_STAGE_NUMBER = "StageNumber";

        private const string URL_ROOT = "http://hallofbeorn.com";
        #endregion

        private string GetFrontImagePath(Card card)
        {
            var viewModel = new CardViewModel(card);
            return (!string.IsNullOrEmpty(viewModel.ImagePath1)) ?
                viewModel.ImagePath1
                : viewModel.ImagePath;
        }

        private string GetBackImagePath(Card card)
        {
            var viewModel = new CardViewModel(card);

            return (!string.IsNullOrEmpty(viewModel.ImagePath2)) ?
                viewModel.ImagePath2
                : null;
        }

        private void InitializeHero(Card card)
        {
            this.Front.Stats[STAT_THREAT_COST] = card.ThreatCost.ToString();
            this.Front.Stats[STAT_WILLPOWER] = card.Willpower.ToString();
            this.Front.Stats[STAT_ATTACK] = card.Attack.ToString();
            this.Front.Stats[STAT_DEFENSE] = card.Defense.ToString();
            this.Front.Stats[STAT_HIT_POINTS] = !card.HitPoints.HasValue ? "X" : card.HitPoints.ToString();
        }

        private void InitializeAlly(Card card)
        {
            this.Front.Stats[STAT_RESOURCE_COST] = card.IsVariableCost || !card.ResourceCost.HasValue ? "X" : card.ResourceCost.Value.ToString();
            this.Front.Stats[STAT_WILLPOWER] = card.Willpower.ToString();
            this.Front.Stats[STAT_ATTACK] = card.IsVariableAttack ? "X" : card.Attack.ToString();
            this.Front.Stats[STAT_DEFENSE] = card.Defense.ToString();
            this.Front.Stats[STAT_HIT_POINTS] = !card.HitPoints.HasValue ? "X" : card.HitPoints.Value.ToString();
        }

        private void InitializeAttachment(Card card)
        {
            this.Front.Stats[STAT_RESOURCE_COST] = card.IsVariableCost || !card.ResourceCost.HasValue ? "X" : card.ResourceCost.Value.ToString();
        }

        private void InitializeEvent(Card card)
        {
            this.Front.Stats[STAT_RESOURCE_COST] = card.IsVariableCost || !card.ResourceCost.HasValue ? "X" : card.ResourceCost.Value.ToString();
        }

        private void InitializeEnemy(Card card)
        {
            this.Front.Stats[STAT_ENGAGEMENT_COST] = card.EngagementCost.HasValue ? card.EngagementCost.Value.ToString() : "X";
            this.Front.Stats[STAT_THREAT] = card.IsVariableThreat ? "X" : card.Threat.ToString();
            this.Front.Stats[STAT_ATTACK] = card.IsVariableAttack ? "X" : card.Attack.ToString();
            this.Front.Stats[STAT_DEFENSE] = card.Defense.ToString();
            this.Front.Stats[STAT_HIT_POINTS] = card.IsVariableHitPoints || !card.HitPoints.HasValue ? "X" : card.HitPoints.Value.ToString();
        }

        private void InitializeLocation(Card card)
        {
            this.Front.Stats[STAT_THREAT] = card.IsVariableThreat ? "X" : card.Threat.ToString();
            this.Front.Stats[STAT_QUEST_POINTS] = card.IsVariableQuestPoints || !card.QuestPoints.HasValue ? "X" : card.QuestPoints.Value.ToString();
        }

        private void InitializeEncounterSet(Card card)
        {
            if (this.EncounterInfo == null)
            {
                this.EncounterInfo = new SimpleEncounterInfo();
            }

            this.EncounterInfo.EncounterSet = card.EncounterSet;

            if (card.EasyModeQuantity.HasValue)
            {
                this.EncounterInfo.EasyModeQuantity = card.EasyModeQuantity.Value;
            }
        }

        private void InitializeObjective(Card card)
        {
            if (card.HitPoints > 0)
            {
                this.Front.Stats[STAT_WILLPOWER] = card.Willpower.ToString();
                this.Front.Stats[STAT_ATTACK] = card.IsVariableAttack ? "X" : card.Attack.ToString();
                this.Front.Stats[STAT_DEFENSE] = card.Defense.ToString();
                this.Front.Stats[STAT_HIT_POINTS] = !card.HitPoints.HasValue ? "X" : card.HitPoints.Value.ToString();
            }
        }

        private void InitializeBoon(Card card)
        {
            if (card.HitPoints > 0)
            {
                this.Front.Stats[STAT_WILLPOWER] = card.Willpower.ToString();
                this.Front.Stats[STAT_ATTACK] = card.IsVariableAttack ? "X" : card.Attack.ToString();
                this.Front.Stats[STAT_DEFENSE] = card.Defense.ToString();
                this.Front.Stats[STAT_HIT_POINTS] = !card.HitPoints.HasValue ? "X" : card.HitPoints.Value.ToString();
            }

            this.Front.Stats[STAT_RESOURCE_COST] = !card.ResourceCost.HasValue ? "X" : card.ResourceCost.Value.ToString();
        }

        private void InitializeQuest(Card card)
        {
            this.Back = new Side() { ImagePath = GetBackImagePath(card) };

            this.Front.Stats[STAT_STAGE_NUMBER] = card.StageNumber + "A";
            this.Back.Stats[STAT_STAGE_NUMBER] = card.StageNumber + "B";
            this.Back.Stats[STAT_QUEST_POINTS] = card.QuestPoints.HasValue ? card.QuestPoints.Value.ToString() : "-";

            foreach (var encounterSet in card.IncludedEncounterSets)
            {
                this.EncounterInfo.IncludedEncounterSets.Add(encounterSet.Name);
            }
        }

        private void InitializeText(Card card)
        {
            if (!string.IsNullOrEmpty(card.Text))
            {
                foreach (var effect in card.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.Front.Text.Add(effect.Replace('"', '`').Replace("~", string.Empty));
                }
            }

            if (!string.IsNullOrEmpty(card.FlavorText))
            {
                this.Front.FlavorText = card.FlavorText.Replace('"', '`');
            }

            if (!string.IsNullOrEmpty(card.OppositeText))
            {
                if (this.Back == null)
                {
                    this.Back = new Side();
                }

                foreach (var effect in card.OppositeText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.Back.Text.Add(effect.Replace('"', '`').Replace("~", string.Empty));
                }
            }

            if (!string.IsNullOrEmpty(card.OppositeFlavorText))
            {
                if (this.Back == null)
                {
                    this.Back = new Side();
                }

                this.Back.FlavorText = card.OppositeFlavorText.Replace('"', '`');
            }
        }
    }
}