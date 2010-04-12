using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IKey :
        IEquatable<IKey>
    {
        ITable Table { get; }
        string Name { get; }
        KeyType KeyType { get; }
        ISet<IColumn> Columns { get; }
        ISet<IColumn> References { get; }
    }
}
