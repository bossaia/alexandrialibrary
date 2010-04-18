using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IInsert : IStatement
    {
        ConflictResponse OnConflict { get; }
        bool Replace { get; }
        bool Default { get; }
        ISource Source { get; }
        ISet<IColumn> Columns { get; }
        IMap<string, object> Data { get; }
        ISelect Select { get; }

    }
}
