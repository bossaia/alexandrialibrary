﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheBloodOfGondorProduct : Product
    {
        public TheBloodOfGondorProduct()
            : base("The Blood of Gondor", "MEC22", ImageType.Png)
        {
            CardSets.Add(new Sets.TheBloodofGondor());
        }
    }
}