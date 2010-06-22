using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IAlterTable
    {
        ITable Table { get; }
        string Rename { get; }
        ISet<IColumn> AddColumns { get; }
        ISet<IColumn> DropColumns { get; }
        ISet<IAlterColumn> AlterColumns { get; }
        ISet<IConstraint> AddConstraints { get; }
        ISet<IConstraint> DropConstraints { get; }
        ISet<IConstraint> AlterConstrains { get; }
    }
}
