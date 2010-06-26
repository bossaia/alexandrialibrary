using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public enum ReferenceOption
    {
        /// <summary>
        /// None - No Action
        /// </summary>
        None = 0,
        Restrict,
        Cascade,
        SetDefault,
        SetNull
    }
}
