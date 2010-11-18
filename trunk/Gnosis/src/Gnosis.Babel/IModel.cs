namespace Gnosis.Babel
{
    public interface IModel
    {
        object Id { get; }
        bool IsDeleted { get; }
        bool IsNew { get; }
        void Delete();
        void Initialize(object id);
    }
}
