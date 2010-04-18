using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IDatabase
    {
        string Name { get; }
        ISet<IIndex> Indices { get; }
        ISet<ITable> Tables { get; }
        ISet<IView> Views { get; }
    }
}
