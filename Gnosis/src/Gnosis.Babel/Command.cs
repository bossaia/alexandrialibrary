using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Babel
{
    public class Command : ICommand
    {
        public Command()
        {
        }

        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        private readonly IList<IStatement> _statements = new List<IStatement>();
        private Action<IModel, object> _callback { get; set; }
        private IModel _model { get; set; }

        private void AddParameter(string name, object value)
        {
            if (!_parameters.ContainsKey(name))
                _parameters.Add(name, value);
        }

        private void AddParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters != null)
                foreach (var item in parameters)
                    AddParameter(item.Key, item.Value);
        }

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return _parameters; }
        }

        public void AddStatement(IStatement statement)
        {
            _statements.Add(statement);
            AddParameters(statement.Parameters);
        }

        public void InvokeCallback(object value)
        {
            if (_callback != null && _model != null)
                _callback(_model, value);
        }

        public void SetCallback(Action<IModel, object> callback, IModel model)
        {
            _callback = callback;
            _model = model;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            _statements.Each(x => builder.Append(x.ToString() + Environment.NewLine));

            return builder.ToString();
        }
    }
}
