using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
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
