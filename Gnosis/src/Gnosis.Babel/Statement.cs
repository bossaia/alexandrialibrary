using System.Collections.Generic;
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
        private bool _listMode = false;
        private bool _hasParentheses = false;

        private void AddParameter(string name, object value)
        {
            if (!name.StartsWith("@"))
                name = string.Format("@{0}", name);

            _parameters[name] = value;
        }

        private void AddParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            parameters.Each(x => AddParameter(x.Key, x.Value));
        }

        #region Fluent Append Methods

        protected TInterface AppendWord<TInterface, TConcrete>(string word)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendWord(word);

            return concrete;
        }

        protected TInterface AppendClause<TInterface, TConcrete>(string clause)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendClause(clause);

            return concrete;
        }

        protected TInterface AppendListItem<TInterface, TConcrete>(string item)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendListItem(item);

            return concrete;
        }

        protected TInterface AppendParentheticalListItem<TInterface, TConcrete>(string item)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParentheticalListItem(item);

            return concrete;
        }

        protected TInterface AppendParameter<TInterface, TConcrete>(string name, object value)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParameter(name, value);

            return concrete;
        }

        #endregion

        #region Regular Append Methods

        protected void AppendWord(string word)
        {
            if (_text.Length > 0)
                _text.AppendFormat(" {0}", word);
            else _text.Append(word);
        }

        protected void AppendListItem(string item)
        {
            if (_listMode)
                _text.AppendFormat(", {0}", item);
            else if (_text.Length > 0)
                _text.AppendFormat(" {0}", item);
            else
                _text.Append(item);

            _listMode = true;
            _hasParentheses = false;
        }

        protected void AppendClause(string clause)
        {
            if (_listMode && _hasParentheses)
                _text.Append(")");

            if (_text.Length > 0)
                _text.AppendFormat(" {0}", clause);
            else
                _text.Append(clause);

            _listMode = false;
            _hasParentheses = false;
        }

        protected void AppendParameter(string name, object value)
        {
            AppendWord(name);
            AddParameter(name, value);
        }

        protected void AppendParentheticalListItem(string item)
        {
            if (_listMode)
                _text.AppendFormat(", {0}", item);
            else
                _text.AppendFormat(" ({0}", item);

            _listMode = true;
            _hasParentheses = true;
        }

        #endregion

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return _parameters; }
        }

        public override string ToString()
        {
            var suffix = (_listMode && _hasParentheses) ? ");" : ";";
            return _text.ToString() + suffix;
        }
    }
}
