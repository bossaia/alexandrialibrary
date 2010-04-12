using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IDomain
    {
        IDatabase Database { get; }
        string Name { get; }
        Type BaseType { get; }
        object Default { get; }
        ITuple<IRule<object>> Rules { get; }

        void SetDatabase(IDatabase database);
    }
}
