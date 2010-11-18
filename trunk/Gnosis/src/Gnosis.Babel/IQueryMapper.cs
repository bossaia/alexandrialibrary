namespace Gnosis.Babel
{
    public interface IQueryMapper<T>
    {
        ICommand GetSelectOneCommand(object id);
        ICommand GetSelectAllCommand();
    }
}
