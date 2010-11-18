namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertValue : IStatement, IInsertValued
    {
    }

    public interface IInsertValue<T> : IStatement, IInsertValued<T>
    {
    }

}
