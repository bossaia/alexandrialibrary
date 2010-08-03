using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IConstraint<T> :
        INamed,
        IExpression<T, bool>
    {
    }
}
