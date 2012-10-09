﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public interface IInPlay<T>
        where T : ICard
    {
        T Card { get; }
    }
}
