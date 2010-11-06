using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CommandBuilder : ICommandBuilder
    {
        public CommandBuilder()
        {
        }

        public CommandBuilder(Action<IModel, object> callback)
        {
            _callback = callback;
        }

        private readonly StringBuilder _text = new StringBuilder();
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        private readonly Action<IModel, object> _callback;

        public CommandBuilder Append(string value)
        {
            _text.Append(value);
            return this;
        }

        public CommandBuilder AppendFormat(string format, params object[] args)
        {
            _text.AppendFormat(format, args);
            return this;
        }

        public CommandBuilder AppendParameterReference(string name, object value)
        {
            var parameterName = string.Format("@{0}", name);
            if (!_parameters.ContainsKey(parameterName))
                _parameters.Add(parameterName, value);

            Append(parameterName);
            return this;
        }

        public ICommand ToCommand()
        {
            if (_text.Length > 0 && _text[_text.Length - 1] != ';')
                _text.Append(';');

            return new Command(_text.ToString(), _parameters, _callback);
        }
    }
}
