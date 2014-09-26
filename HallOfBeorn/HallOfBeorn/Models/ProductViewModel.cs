﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product)
        {
            _product = product;

            //var scenarioTitle = string.Empty;
            //const string linkFormat = "/Scenarios/Details/{0}";

            /*
            foreach (var cardSet in product.CardSets)
            {
                foreach (var card in cardSet.Cards.Where(x => (x.CardType == CardType.Quest || (x.EncounterSet != null && x.EncounterSet.EndsWith("Nightmare"))) && !string.IsNullOrEmpty(x.EncounterSet)))
                {
                    scenarioTitle = !string.IsNullOrEmpty(card.ScenarioTitle) ? card.ScenarioTitle : card.EncounterSet;

                    if (!_scenarios.Any(x => x.Title == scenarioTitle))
                    {
                        _scenarios.Add(new ScenarioViewModel { Title = scenarioTitle, Link = string.Format(linkFormat, scenarioTitle.ToUrlSafeString()) });
                    }
                }
            }*/
        }

        private readonly Product _product;
        private readonly List<ScenarioViewModel> _scenarios = new List<ScenarioViewModel>();

        public string Name { get { return _product.Name; } }
        public string Code { get { return _product.Code; } }
        public bool IsPremier { get { return _product.IsPremier; } }

        public string ImagePath
        {
            get
            {
                var ext = (_product.ImageType == ImageType.Jpg) ? "jpg" : "png";

                return string.Format("/Images/Products/{0}.{1}", _product.Code, ext);
            }
        }
        public int ImageWidth
        {
            get
            {
                return _product.CardSets.Any(x => x.SetType == SetType.Core || x.SetType == SetType.Deluxe_Expansion || x.SetType == SetType.Saga_Expansion) ?
                    235 : 142;
            }
        }
        public int ImageHeight
        {
            get
            {
                return 235;
            }
        }
        
        public string Link
        {
            get
            {
                var cardSet = string.Empty;

                var first = _product.CardSets.FirstOrDefault();
                if (first != null)
                {
                    cardSet = first.Name.Replace(' ', '+');
                }

                return string.Format("/Cards/Search?CardSet={0}", cardSet);
            }
        }

        public IEnumerable<ScenarioViewModel> Scenarios
        {
            get { return _scenarios; }
        }
    }
}