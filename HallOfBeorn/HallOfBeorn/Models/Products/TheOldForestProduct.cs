﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheOldForestProduct : Product
    {
        public TheOldForestProduct()
            : base("The Old Forest", "MEC37", ImageType.Jpg)
        {
            CardSets.Add(new Sets.TheOldForest());
        }
    }
}