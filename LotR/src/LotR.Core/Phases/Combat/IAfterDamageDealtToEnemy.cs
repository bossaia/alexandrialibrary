﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface IAfterDamageDealtToEnemy
    {
        void AfterDamageDealtToEnemy(IDealDamageToEnemyStep step);
    }
}
