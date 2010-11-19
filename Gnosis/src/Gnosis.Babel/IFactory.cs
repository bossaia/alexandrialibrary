namespace Gnosis.Babel
{
    public interface IFactory<in T>
    {
        T Create();
    }
}
