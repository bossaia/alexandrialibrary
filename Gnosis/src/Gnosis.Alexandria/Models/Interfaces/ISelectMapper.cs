using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ISelectMapper<T>
        where T : IModel
    {
        ICommand GetSelectOneCommand(object id);
        ICommand GetSelectAllCommand();
    }
}
