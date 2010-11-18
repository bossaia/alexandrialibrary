using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertColumn : IStatement, IInsertColumnar, IInsertValued
    {
        ISelect Select { get; }
    }

    public interface IInsertColumn<T> : IStatement, IInsertColumnar<T>, IInsertValued<T>
    {
        ISelect Select { get; }
    }
}
