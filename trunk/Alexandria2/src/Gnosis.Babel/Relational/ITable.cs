using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ITable
    {
        IDatabase Database { get; }
        string Name { get; }
        ISet<IColumn> Columns { get; }
        ISet<IKey> Keys { get; }

        void SetDatabase(IDatabase database);
    }
}
