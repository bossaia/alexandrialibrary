using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IConstraint
    {
        ITable Table { get; }
        string Name { get; }
        ConstraintType Type { get; }
        IExpression Expression { get; }
        ISet<IColumn> Columns { get; }
    }
}
