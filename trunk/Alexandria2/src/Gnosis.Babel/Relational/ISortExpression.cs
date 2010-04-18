using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface ISortExpression
    {
        IColumn Column { get; }
        bool IsAsending { get; }
    }
}
