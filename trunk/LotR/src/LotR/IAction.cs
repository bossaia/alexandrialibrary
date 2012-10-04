﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IAction
        : IActiveEffect
    {
        void Resolve(IPhaseStep step, IPayment payment);
    }
}
