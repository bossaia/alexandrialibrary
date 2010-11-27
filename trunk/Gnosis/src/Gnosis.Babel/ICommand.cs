using System;
using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface ICommand
    {
        Guid Id { get; }
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        void AddStatement(IStatement statement);
        void SetParameter(string name, object value);
        string ToString();
    }
}
