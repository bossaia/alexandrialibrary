using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface ITable :
        IDatabaseObject,
        ICreatable,
        IDropable,
        INamed,
        IRenameable,
        IInsertable,
        IUpdateable,
        IDeleteable
    {
        IEnumerable<IColumn> Columns { get; }
        IEnumerable<ITableConstraint> Constraints { get; }
    }
}
