using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IQuery
    {
        int Limit { get; }
        int Offset { get; }
        ISet<IColumn> Selection { get; }
        Predicate<object> Predicate { get; }
        ISet<IColumn> Grouping { get; }
        ISet<IColumn> Sorting { get; }
    }
}
