﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public class AmbushEffect
        : CardEffectBase, IAmbushEffect
    {
        public AmbushEffect(ICard cardSource)
            : base(EffectType.Ambush, "After this enemy enters play, each player makes an engagement check against it", cardSource)
        {
        }

        public override string ToString()
        {
            return "Ambush (after this enemy enters play, each player makes an engagement check against it.)";
        }
    }
}
