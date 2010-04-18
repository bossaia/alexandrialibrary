using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ISource
    {
        bool Temporary { get; }
        IDatabase Database { get; }
        string Name { get; }
        ISet<IColumnExpression> Columns { get; }
    }
}
