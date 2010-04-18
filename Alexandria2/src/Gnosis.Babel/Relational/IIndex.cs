using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IIndex
    {
        bool Unique { get; }
        IDatabase Database { get; }
        string Name { get; }
        ITable On { get; }
        ISet<IColumn> Columns { get; }
    }
}
