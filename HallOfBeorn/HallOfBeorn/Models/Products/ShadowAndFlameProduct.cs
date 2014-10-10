﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class ShadowAndFlameProduct : Product
    {
        public ShadowAndFlameProduct()
            : base("Shadow and Flame", "MEC14", ImageType.Png)
        {
            RulesUrl = "http://www.fantasyflightgames.com/ffg_content/lotr-lcg/support/rulesheets/Shadow_and_Flame_rulesheet.pdf";

            CardSets.Add(CardSet.ShadowAndFlame);
        }
    }
}