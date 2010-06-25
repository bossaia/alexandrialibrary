using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface ITable
        : INamed
    {
        IDatabase Database { get; }
        IEnumerable<IColumn> Columns();
    }
}
