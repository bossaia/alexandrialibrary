namespace Gnosis.Babel
{
    public class GenericFactory<T, C> : IFactory<T>
        where C : T, new()
    {
        public T Create()
        {
            return new C();
        }
    }
}
