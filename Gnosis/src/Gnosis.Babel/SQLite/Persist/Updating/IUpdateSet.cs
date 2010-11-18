namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdateSet : IStatement
    {
        IUpdateColumn Set { get; }
    }

    public interface IUpdateSet<T> : IStatement
    {
        IUpdateColumn<T> Set { get; }
    }
}
