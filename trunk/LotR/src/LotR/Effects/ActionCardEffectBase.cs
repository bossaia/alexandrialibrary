﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class ActionCardEffectBase
        : CardEffectBase, IActionEffect
    {
        protected ActionCardEffectBase(string description, ICard cardSource)
            : base(description, cardSource)
        {
        }

        public override string ToString()
        {
            return string.Format("Action: {0}", Description);
        }
    }
}