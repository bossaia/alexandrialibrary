using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public abstract class CommandBuilder : ICommandBuilder
    {
        protected CommandBuilder(IFactory<ICommand> factory)
        {
            _factory = factory;
        }

        private readonly IFactory<ICommand> _factory;
        private readonly StringBuilder _text = new StringBuilder();
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        private Action<IModel, object> _callback;

        protected void Append(string value)
        {
            _text.Append(value);
        }

        protected void AppendFormat(string format, params object[] args)
        {
            _text.AppendFormat(format, args);
        }

        protected void AppendLine(string value)
        {
            _text.AppendLine(value);
        }

        protected void AppendParameter(string name, object value)
        {
            var parameterName = string.Format("@{0}", name);
            if (!_parameters.ContainsKey(parameterName))
                _parameters.Add(parameterName, value);

            Append(parameterName);
        }

        protected void SetCallback(Action<IModel, object> callback)
        {
            _callback = callback;
        }

        public ICommand ToCommand()
        {
            if (_text.Length > 0 && _text[_text.Length - 1] != ';')
                _text.Append(';');

            var command = _factory.Create();

            command.Text = _text.ToString();
            command.AddParameters(_parameters);
            command.Callback = _callback;

            return command;
        }
    }
}
