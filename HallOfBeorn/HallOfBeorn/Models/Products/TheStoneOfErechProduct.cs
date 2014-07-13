﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheStoneOfErechProduct : Product
    {
        public TheStoneOfErechProduct()
            : base("The Stone of Erech GenCon 2013", "MEC33", ImageType.Jpg)
        {
            CardSets.Add(CardSet.TheStoneOfErech);
        }
    }
}