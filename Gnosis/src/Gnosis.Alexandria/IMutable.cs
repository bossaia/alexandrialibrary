namespace Gnosis.Alexandria
{
    public interface IMutable<T> : IStateful<T>
        where T : IMessage
    {
        void Change(T state);
    }
}
