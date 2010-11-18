using System;
using System.Collections.Generic;

namespace Gnosis.Babel
{
    public class Command : ICommand
    {
        public Command()
        {
        }

        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();

        public string Text { get; set; }

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return _parameters; }
        }

        public Action<IModel, object> Callback { get; set; }
        public IModel Model { get; set; }

        public void CallbackWithResult(object value)
        {
            if (Callback != null && Model != null)
                Callback(Model, value);
        }

        public void AddParameter(string name, object value)
        {
            if (!_parameters.ContainsKey(name))
                _parameters.Add(name, value);
        }

        public void AddParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters != null)
                foreach (var item in parameters)
                    _parameters.Add(item);
        }
    }
}
