using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ISchemaMapper<T>
        where T : ISchema
    {
        IEnumerable<ICommand> GetInitializeCommands();
    }
}
