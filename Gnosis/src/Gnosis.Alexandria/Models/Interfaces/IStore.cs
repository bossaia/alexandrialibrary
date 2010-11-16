using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IStore
    {
        string Name { get; }
        void Execute(IEnumerable<ICommand> commands);
        ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper) where T :IModel;
        ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper, ICache<T> cache) where T : IModel;
    }
}
