namespace Gnosis.Babel
{
    public interface IPersistMapper<T>
    {
        ICommand GetPersistCommand(T model);
    }
}
