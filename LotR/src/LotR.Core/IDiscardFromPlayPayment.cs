﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IDiscardFromPlayPayment
        : IPayment
    {
        ICardInPlay Card { get; }
    }
}
