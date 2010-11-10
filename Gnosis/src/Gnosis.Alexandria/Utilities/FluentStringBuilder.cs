using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Utilities.Interfaces;

namespace Gnosis.Alexandria.Utilities
{
    public class FluentStringBuilder : IStringBuilder
    {
        public FluentStringBuilder()
        {
            ClauseDelimiter = ", ";
            TokenDelimiter = " ";
        }

        public FluentStringBuilder(string clauseDelimiter, string tokenDelimiter)
        {
            ClauseDelimiter = clauseDelimiter;
            TokenDelimiter = tokenDelimiter;
        }

        private readonly StringBuilder _builder = new StringBuilder();

        public string ClauseDelimiter { get; set; }

        public string TokenDelimiter { get; set; }

        public IStringBuilder Append(string value)
        {
            _builder.Append(value);
            return this;
        }

        public IStringBuilder Append(IStringBuilder builder)
        {
            if (builder == null)
                return this;

            _builder.Append(builder.ToString());
            return this;
        }

        public IStringBuilder AppendClause(params string[] tokens)
        {
            if (_builder.Length > 0)
                Append(ClauseDelimiter);

            var first = true;
            foreach (var token in tokens)
            {
                if (!first)
                    Append(TokenDelimiter);

                Append(token);
                first = false;
            }

            return this;
        }

        public IStringBuilder AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
            return this;
        }

        public IStringBuilder AppendLine(string value)
        {
            _builder.AppendLine(value);
            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
