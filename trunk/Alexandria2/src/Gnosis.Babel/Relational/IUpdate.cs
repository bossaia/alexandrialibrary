using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IUpdate : IStatement
    {
        ConflictResponse OnConflict { get; }
        int Limit { get; }
        int Offset { get; }
        ISource Source { get; }
        IMap<IColumn, IExpression> Set { get; }
        IExpression Where { get; }
        ISet<ISortExpression> OrderBy { get; }
    }
}
