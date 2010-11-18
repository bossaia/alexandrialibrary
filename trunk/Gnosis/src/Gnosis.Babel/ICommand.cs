using System;
using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface ICommand
    {
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        void AddStatement(IStatement statement);
        void InvokeCallback(object value);
        void SetCallback(Action<IModel, object> callback, IModel model);
        string ToString();
    }
}
