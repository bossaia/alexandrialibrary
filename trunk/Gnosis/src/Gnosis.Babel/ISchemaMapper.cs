using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface ISchemaMapper<T>
    {
        IEnumerable<ICommand> GetInitializeCommands();
    }
}
