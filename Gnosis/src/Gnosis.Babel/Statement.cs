using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public abstract class Statement : IStatement
    {
        protected Statement()
        {
            _text = new StringBuilder();
        }

        protected Statement(string value)
        {
            _text = new StringBuilder(value);
        }

        protected Statement(string value, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            _text = new StringBuilder(value);

            if (parameters != null)
                foreach (var item in parameters)
                    _parameters.Add(item);
        }

        private readonly StringBuilder _text;
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        private bool listMode = false;
        private bool hasParentheses = false;

        private void AddParameter(string name, object value)
        {
            _parameters[name] = value;
        }

        protected void AppendWord(string word)
        {
            if (_text.Length > 0)
                _text.AppendFormat(" {0}", word);
            else _text.Append(word);
        }

        protected void AppendListItem(string item)
        {
            if (listMode)
                _text.AppendFormat(", {0}", item);
            else if (_text.Length > 0)
                _text.AppendFormat(" {0}", item);
            else
                _text.Append(item);

            listMode = true;
            hasParentheses = false;
        }

        protected void AppendClause(string clause)
        {
            if (listMode && hasParentheses)
                _text.Append(")");

            if (_text.Length > 0)
                _text.AppendFormat(" {0}", clause);
            else
                _text.Append(clause);

            listMode = false;
            hasParentheses = false;
        }

        protected void AppendParameter(string name, object value)
        {
            AddParameter(name, value);
            AppendWord(name);
        }

        protected void AppendParentheticalListItem(string item)
        {
            if (listMode)
                _text.AppendFormat(", {0}", item);
            else
                _text.AppendFormat(" ({0}", item);

            listMode = true;
            hasParentheses = true;
        }

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return _parameters; }
        }

        public override string ToString()
        {
            var suffix = (listMode && hasParentheses) ? ");" : ";";
            return _text.ToString() + suffix;
        }
    }
}
