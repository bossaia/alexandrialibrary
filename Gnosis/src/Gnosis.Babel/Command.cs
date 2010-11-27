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

        private readonly Guid _id = Guid.NewGuid();
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        private readonly IList<IStatement> _statements = new List<IStatement>();

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

        public Guid Id
        {
            get { return _id; }
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

        public void SetParameter(string name, object value)
        {
            if (_parameters.ContainsKey(name) && _parameters[name] != null)
                _parameters[name] = value;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            _statements.Each(x => builder.Append(x.ToString() + Environment.NewLine));

            return builder.ToString();
        }
    }
}
