namespace Gnosis.Alexandria
{
    public interface IStateful<T>
        where T : IMessage
    {
        T State { get; }
    }
}
