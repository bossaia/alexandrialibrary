using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICommandBuilder
    {
        ICommandBuilder Append(string value);
        ICommandBuilder AppendFormat(string format, params object[] args);
        ICommandBuilder AppendLine(string value);
        ICommandBuilder AppendParameter(string name, object value);
        ICommandBuilder SetCallback(Action<IModel, object> callback);
        ICommand ToCommand();
    }
}
