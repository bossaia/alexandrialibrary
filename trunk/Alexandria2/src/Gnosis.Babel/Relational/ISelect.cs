using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ISelect : IStatement
    {
        int Limit { get; }
        int Offset { get; }
        bool Distinct { get; }
        ISet<IColumnExpression> Columns { get; }
        ISet<IJoin> From { get; }
        IExpression Where { get; }
        ISet<IColumn> GroupBy { get; }
        IExpression Having { get; }
        ISet<ISortExpression> OrderBy { get; }
    }
}
