using System;
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
        private bool _subListMode = false;
        private bool _hasParentheses = false;
        private bool _hasSubParentheses = false;

        private static string GetParameterName(string name)
        {
            if (!name.StartsWith("@"))
                return string.Format("@{0}", name);

            return name;
        }

        private void AddParameter(string name, object value)
        {
            _parameters[name] = value;
        }

        private void AddParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            parameters.Each(x => AddParameter(x.Key, x.Value));
        }

        private Tuple<int, int> GetNumberOfOpenAndClosedParentheses()
        {
            var openCount = 0;
            var closedCount = 0;
            foreach (var character in _text.ToString().ToCharArray())
            {
                if (character == '(') openCount++;
                else if (character == ')') closedCount++;
            }

            return new Tuple<int, int>(openCount, closedCount);
        }

        private bool HasOpenParentheses
        {
            get
            {
                var count = GetNumberOfOpenAndClosedParentheses();
                return (count.Item1 > count.Item2);
            }
        }

        #region Fluent Append Methods

        protected TInterface Transform<TInterface, TConcrete>()
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());

            //TODO: Refactor this - transfering object state like this is *UGLY*
            concrete._listMode = _listMode;
            concrete._hasParentheses = _hasParentheses;
            concrete._subListMode = _subListMode;
            concrete._hasSubParentheses = _hasSubParentheses;

            return concrete;
        }

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

        protected TInterface AppendParentheticalListItem<TInterface, TConcrete>(string name, object value)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParentheticalListItem(name, value);

            return concrete;
        }

        protected TInterface AppendParentheticalSubListItem<TInterface, TConcrete>(string item)
            where TInterface : IStatement
            where TConcrete : Statement, TInterface, new()
        {
            var concrete = new TConcrete();
            concrete.AddParameters(_parameters);
            concrete.AppendWord(_text.ToString());
            concrete.AppendParentheticalSubListItem(item);

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
            if (_subListMode && _hasSubParentheses)
                _text.Append(")");

            if (_text.Length > 0)
                _text.AppendFormat(" {0}", word);
            else _text.Append(word);

            _subListMode = false;
            _hasSubParentheses = false;
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
            if (_subListMode && _hasSubParentheses)
                _text.Append(")");

            if (_listMode && _hasParentheses)
                _text.Append(")");

            if (_text.Length > 0)
                _text.AppendFormat(" {0}", clause);
            else
                _text.Append(clause);

            _listMode = false;
            _hasParentheses = false;
            _subListMode = false;
            _hasSubParentheses = false;
        }

        protected string GetAnonymousParameterName()
        {
            return "@" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        protected void AppendParameter(string name, object value)
        {
            var parameterName = GetParameterName(name);

            AppendWord(parameterName);
            AddParameter(parameterName, value);
        }

        protected void AppendParentheticalListItem(string item)
        {
            if (_subListMode)
            {
                _text.Append(")");
                _subListMode = false;
                _hasSubParentheses = false;
            }

            if (_listMode || HasOpenParentheses)
                _text.AppendFormat(", {0}", item);
            else
                _text.AppendFormat(" ({0}", item);

            _listMode = true;
            _hasParentheses = true;
        }

        protected void AppendParentheticalListItem(string name, object value)
        {
            if (_subListMode)
            {
                _text.Append(")");
                _subListMode = false;
                _hasSubParentheses = false;
            }

            if (_listMode || HasOpenParentheses)
                _text.Append(", ");
            else
                _text.Append(" (");

            AppendParameter(name, value);

            _listMode = true;
            _hasParentheses = true;
        }

        protected void AppendParentheticalSubListItem(string item)
        {
            if (_subListMode)
                _text.AppendFormat(", {0}", item);
            else
                _text.AppendFormat(" ({0}", item);

            _subListMode = true;
            _hasSubParentheses = true;
        }

        #endregion

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return _parameters; }
        }

        public override string ToString()
        {
            var suffix = string.Empty;
            var count = GetNumberOfOpenAndClosedParentheses();
            var closed = 0;

            while (count.Item1 > count.Item2 + closed)
            {
                suffix += ")";
                closed++;
            }

            return _text.ToString() + suffix + ";";
        }
    }
}
