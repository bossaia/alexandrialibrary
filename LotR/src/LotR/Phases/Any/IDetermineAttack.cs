﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface IDetermineAttack
    {
        void DetermineAttack(IDetermineAttackStep step);
    }
}
