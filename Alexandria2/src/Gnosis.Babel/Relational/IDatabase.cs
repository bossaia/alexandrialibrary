using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IDatabase :
        INamed
    {
        IEnumerable<IIndex> Indices { get; }
        IEnumerable<ITable> Tables { get; }

        void ExecuteCommands(IEnumerable<string> commands);
        ISet<ISet<ITuple>> ExecuteQueries(IEnumerable<string> queries);
    }
}
