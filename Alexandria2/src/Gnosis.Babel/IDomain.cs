using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IDomain :
        INamed
    {
        ISet<IConstraint<object>> Constraints { get; }
    }
}
