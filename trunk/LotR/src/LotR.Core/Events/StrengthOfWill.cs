﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class StrengthOfWill
        : EventCardBase
    {
        public StrengthOfWill()
            : base("Strength of Will", SetNames.Core, 47, Sphere.Spirit, 0)
        {
        }
    }
}
