using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Commands
{
    public class Clause : IClause
    {
        public Clause(string name)
        {
            _name = name;
            _builder = new FluentStringBuilder();
        }

        public Clause(string name, string clauseDelimiter, string tokenDelimiter)
        {
            _name = name;
            _builder = new FluentStringBuilder(clauseDelimiter, tokenDelimiter);
        }

        private readonly string _name;
        private readonly FluentStringBuilder _builder;

        #region IClause Members

        public string Name
        {
            get { return _name; }
        }

        public void Append(params string[] tokens)
        {
            _builder.AppendClause(tokens);
        }

        public void Append<T>(Expression<Func<T, object>> expression) where T : IModel
        {
            _builder.Append(GetName<T>(expression));
        }

        public void Append<T>(Expression<Func<T, object>> expression, string alias) where T : IModel
        {
            _builder.Append(GetName<T>(expression, alias));
        }

        public void AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
        }

        public string GetName<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return expression.ToName();
        }

        public string GetName<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return string.Format("{0}.{1}", alias, GetName(expression));
        }

        #endregion

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
