using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public enum ConflictResponse
    {
        Rollback = 0,
        Abort,
        Fail,
        Ignore,
        Replace
    }
}
