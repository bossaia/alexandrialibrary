﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class ScenarioCardViewModel
    {
        public ScenarioCardViewModel(ScenarioCard scenarioCard)
        {
            Title = scenarioCard.Title;
            EncounterSet = scenarioCard.EncounterSet;
            EncounterSetLink = string.Format("/Cards/Search?EncounterSet={0}", scenarioCard.EncounterSet.Replace(" ", "%20"));
            EncounterSetImage = scenarioCard.EncounterSetImage;
            Link = scenarioCard.Link;

            NormalQuantity = scenarioCard.NormalQuantity > 0 ? scenarioCard.NormalQuantity.ToString() : "-";
            NightmareQuantity = scenarioCard.NightmareQuantity > 0 ? scenarioCard.NightmareQuantity.ToString() : "-";
            EasyQuantity = scenarioCard.EasyQuantity > 0 ? scenarioCard.EasyQuantity.ToString() : "-";
        }

        public string Title { get; private set; }
        public string EncounterSet { get; private set; }
        public string EncounterSetLink { get; private set; }
        public string EncounterSetImage { get; private set; }
        public string Link { get; private set; }
        public string NormalQuantity { get; private set; }
        public string NightmareQuantity { get; private set; }
        public string EasyQuantity { get; private set; }
    }
}