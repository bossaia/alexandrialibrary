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
        ISet<IDomain> Domains { get; }
        ISet<ITable> Tables { get; }

        void Initialize();
        IEnumerable<IMap<string, object>> Read(IQuery query);
        void Execute(ITuple<ICommand> commands);
    }
}
