namespace Gnosis.Babel.SQLite.Persist.Deleting
{
    public interface IDelete : IStatement
    {
        IDeleteFrom From(string table);
    }

    public interface IDelete<T> : IStatement
    {
        IDeleteFrom<T> From(string table);
    }
}
