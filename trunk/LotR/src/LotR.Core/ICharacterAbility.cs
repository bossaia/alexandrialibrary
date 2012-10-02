﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICharacterAbility
        : IPlayerCardEffect
    {
        void Resolve(IPhaseStep step, IPayment payment);
    }
}
