﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface IAfterDamageDealt
    {
        void Setup(IDealDamageStep step);
        void Resolve(IDealDamageStep step);
    }
}
