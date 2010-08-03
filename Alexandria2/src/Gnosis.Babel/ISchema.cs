using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface ISchema :
        INamed
    {
        IEnumerable<IEntity> Entities { get; }
        IEnumerable<IConstraint<ISchema>> Constraints { get; }
    }
}
