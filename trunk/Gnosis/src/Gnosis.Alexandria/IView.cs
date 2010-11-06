namespace Gnosis.Alexandria
{
    public interface IView : IProcessor, IDispatcher
    {
        string Title { get; }
        //void Show();
        //void Close();
    }
}
