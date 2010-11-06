using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICommand
    {
        string Text { get; }
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        Action<IModel, object> Callback { get; }
    }
}
