﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Payments
{
    public interface IExhaustCardPayment
        : IPayment
    {
        IExhaustableInPlay Exhaustable { get; }
    }
}
