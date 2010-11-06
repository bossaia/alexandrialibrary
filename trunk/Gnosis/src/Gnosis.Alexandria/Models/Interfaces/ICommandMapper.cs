using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICommandMapper<T>
        where T : IModel
    {
        ICommandBuilder GetCommandBuilder();
        ICommand GetInitializeCommand();
        ICommand GetPersistCommand(T model);
        ICommand GetSelectOneCommand(object id);
        ICommand GetSelectAllCommand();
    }
}
