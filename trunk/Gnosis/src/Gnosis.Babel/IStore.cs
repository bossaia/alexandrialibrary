using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface IStore
    {
        string Name { get; }
        void Execute(IEnumerable<ICommand> commands);
        ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper);
        ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper, ICache<T> cache);
    }
}
