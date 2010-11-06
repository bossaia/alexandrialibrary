using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class Command : ICommand
    {
        public Command(string text)
            : this(text, null, null)
        {
        }

        public Command(string text, IEnumerable<KeyValuePair<string, object>> parameters)
            : this(text, parameters, null)
        {
        }

        public Command(string text, IEnumerable<KeyValuePair<string, object>> parameters, Action<IModel, object> callback)
        {
            _text = text;
            _parameters = parameters ?? Enumerable.Empty<KeyValuePair<string, object>>();
            _callback = callback ?? new Action<IModel, object>((x, y) => {});
        }

        private readonly string _text;
        private readonly IEnumerable<KeyValuePair<string, object>> _parameters;
        private readonly Action<IModel, object> _callback;

        public string Text
        {
            get { return _text; }
        }

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return _parameters; }
        }

        public Action<IModel, object> Callback
        {
            get { return _callback; }
        }
    }
}
