namespace Gnosis.Babel.SQLite.Persist.Deleting
{
    public interface IDelete : IStatement, IFilterable
    {
        IDeleteFrom From(string table);
    }

    public interface IDelete<T> : IStatement, IFilterable
    {
        IDeleteFrom<T> From(string table);
    }
}
