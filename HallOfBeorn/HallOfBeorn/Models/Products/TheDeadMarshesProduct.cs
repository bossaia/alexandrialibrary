﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheDeadMarshesProduct : Product
    {
        public TheDeadMarshesProduct()
            : base("The Dead Marshes", "MEC06", ImageType.Png)
        {
            RulesUrl = "http://www.fantasyflightgames.com/ffg_content/lotr-lcg/support/rulesheets/The_Dead_Marshes_rulesheet.pdf";

            CardSets.Add(CardSet.TheDeadMarshes);
        }
    }
}