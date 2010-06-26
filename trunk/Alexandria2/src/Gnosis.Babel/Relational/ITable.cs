using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ITable
        : INamed, ICreated, IDropped, IAltered<ITable>, IInserted, IUpdated, IDeleted
    {
        IDatabase Database { get; }
        bool IsTemporary { get; }
        IEnumerable<IColumn> GetColumns();
        IEnumerable<IConstraint> GetConstraints();
        IEnumerable<IIndex> GetIndices();

        ITable Column(string name, Type dataType, Size size, bool isRequired, object @default);
        ITable PrimaryKey(string name, bool isAscending, bool isAutoIncrement, params string[] columns);
        ITable UniqueKey(string name, params string[] columns);
        ITable ForeignKey(string name, ITable referenceTable, ReferenceOption onUpdate, ReferenceOption onDelete, string match, DeferralOption deferred, params string[] references);
        ITable Check(string name, string expression);
        ITable NotNull(string name, string column);
        ITable Default(string name, string column, object @default);
        ITable Index(string name, bool isUnique, params string[] indices);
    }
}
