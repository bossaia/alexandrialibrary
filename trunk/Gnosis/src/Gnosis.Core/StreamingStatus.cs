using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public enum StreamingStatus
    {
        None = 0,
        Initializing,
        Healthy,
        Starving
    }
}
