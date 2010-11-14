using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICommand
    {
        string Text { get; set; }
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        Action<IModel, object> Callback { get; set; }
        IModel Model { get; set; }
        void AddParameter(string name, object value);
        void AddParameters(IEnumerable<KeyValuePair<string, object>> parameters);
    }
}
