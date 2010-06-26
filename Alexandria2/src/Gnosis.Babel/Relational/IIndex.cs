using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IIndex
        : INamed, ICreated, IDropped, IAltered<IIndex>
    {
        ITable Table { get; }
        bool IsUnique { get; }
        IEnumerable<IndexedColumn> Columns { get; }
    }
}
