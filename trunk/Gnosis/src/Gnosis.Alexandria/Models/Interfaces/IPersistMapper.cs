using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IPersistMapper<T>
        where T : IModel
    {
        ICommand GetPersistCommand(T model);
    }
}
