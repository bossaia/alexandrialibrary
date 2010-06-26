using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public enum DeferralOption
    {
        /// <summary>
        /// None - Not Deferrable
        /// </summary>
        None = 0,
        Deferrable,
        DeferredInitially,
        DeferredImmediately
    }
}
