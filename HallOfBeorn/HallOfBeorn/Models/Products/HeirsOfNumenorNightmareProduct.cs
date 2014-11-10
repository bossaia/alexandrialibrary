﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class HeirsOfNumenorNightmareProduct : Product
    {
        public HeirsOfNumenorNightmareProduct()
            : base("Heirs of Númenor Nightmare Decks", "MEN21", ImageType.Jpg)
        {
            CardSets.Add(CardSet.PerilInPelargirNightmare);
            CardSets.Add(CardSet.IntoIthilienNightmare);
            CardSets.Add(CardSet.TheSiegeOfCairAndrosNightmare);
        }
    }
}