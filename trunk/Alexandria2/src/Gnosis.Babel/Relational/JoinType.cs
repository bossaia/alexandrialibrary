using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public enum JoinType
    {
        None = 0,
        Cross,
        Inner,
        Left,
        LeftOuter,
        Natural,
        NaturalCross,
        NaturalInner,
        NaturalLeft,
        NaturalLeftOuter,
        NaturalOuter,
        Outer
    }
}
