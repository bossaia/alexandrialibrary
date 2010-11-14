using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;
using Gnosis.Alexandria.Utilities.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public abstract class CommandBuilder : ICommandBuilder
    {
        protected CommandBuilder(IFactory<ICommand> factory)
        {
            _factory = factory;
        }

        private readonly IFactory<ICommand> _factory;
        private readonly IDictionary<string, IClause> _clauseMap = new Dictionary<string, IClause>();
        private readonly IList<IClause> _clauses = new List<IClause>();
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        private Action<IModel, object> _callback;
        private IModel _model;
        private IClause _currentClause;

        #region Inner Classes

        protected static class Clauses
        {
            public const string Select = "select";
            public const string Insert = "insert";
            public const string Update = "update";
            public const string Delete = "delete";
            public const string Create = "create";
            public const string Columns = "columns";
            public const string Values = "values";
            public const string Set = "set";
            public const string ColumnAssignments = "column assignments";
            public const string From = "from";
            public const string Into = "into";
            public const string Table = "table";
            public const string Where = "where";
            public const string GroupBy = "group by";
            public const string Groupings = "groupings";
            public const string OrderBy = "order by";
            public const string Orderings = "orderings";
            public const string Limit = "limit";
            public const string Compound = "compound";
        }

        protected static class Constants
        {
            public const string AllColumns = "*";
            public const string And = "and";
            public const string Ascending = "asc";
            public const string CloseParen = ")";
            public const string ColumnAssignment = "=";
            public const string Comma = ",";
            public const string CommaSpace = ", ";
            public const string Create = "create";
            public const string CrossJoin = "cross join";
            public const string Delete = "delete";
            public const string Descending = "desc";
            public const string Except = " except ";
            public const string From = " from";
            public const string GroupBy = " group by ";
            public const string Having = "having";
            public const string InnerJoin = "inner join";
            public const string Insert = "insert";
            public const string Intersect = " intersect ";
            public const string Into = " into";
            public const string IsEqualTo = "=";
            public const string IsGreaterThan = ">";
            public const string IsGreaterThanOrEqualTo = ">=";
            public const string IsIn = "in";
            public const string IsLessThan = "<";
            public const string IsLessThanOrEqualTo = "<=";
            public const string IsLike = "like";
            public const string IsNotEqualTo = "<>";
            public const string IsNotIn = "not in";
            public const string IsNotLike = "not like";
            public const string IsNotNull = "is not null";
            public const string IsNull = "is null";
            public const string LeftOuterJoin = "left outer join";
            public const string Limit = " limit";
            public const string Offset = "offset";
            public const string On = "on";
            public const string OpenParen = "(";
            public const string Or = "or";
            public const string OrAbort = "or abort";
            public const string OrFail = "or fail";
            public const string OrIgnore = "or ignore";
            public const string OrReplace = "or replace";
            public const string OrRollback = "or rollback";
            public const string OrderBy = " order by ";
            public const string SelectAll = "select all ";
            public const string SelectDistinct = "select distinct ";
            public const string SelectLastInsertedId = "; select last_insert_rowid();";
            public const string Set = "set";
            public const string Space = " ";
            public const string StatementTerminator = ";";
            public const string Temp = "temp";
            public const string Union = " union ";
            public const string UnionAll = " union all ";
            public const string Update = "update";
            public const string Values = " values";
            public const string Where = " where";
        }

        #endregion

        #region Protected Members

        protected void AddSpaceDelimitedClause(string name)
        {
            var clause = new Clause(name, Constants.Space, Constants.Space);
            AddClause(clause);
        }

        protected void AddCommaDelimitedClause(string name)
        {
            var clause = new Clause(name);
            AddClause(clause);
        }

        protected void AddCommaDelimitedClause(string name, string prefix, string suffix)
        {
            var clause = new Clause(name, Constants.CommaSpace, Constants.Space, prefix, suffix);
            AddClause(clause);
        }

        protected void AddClause(string name, string clauseDelimiter, string tokenDelimiter)
        {
            var clause = new Clause(name, clauseDelimiter, tokenDelimiter);
            AddClause(clause);
        }

        protected void AddClause(IClause clause)
        {
            _clauses.Add(clause);
            _clauseMap.Add(clause.Name, clause);
        }

        protected void SetCurrentClause(string name)
        {
            if (!_clauseMap.ContainsKey(name))
                throw new ArgumentException("A clause with the given name does not exist in this command");

            _currentClause = _clauseMap[name];
        }

        private IClause CurrentClause
        {
            get { return _currentClause; }
        }

        protected string GetName<T>(Expression<Func<T, object>> expression) where T : IModel
        {
            return CurrentClause.GetName<T>(expression);
        }

        protected string GetName<T>(Expression<Func<T, object>> expression, string alias) where T : IModel
        {
            return CurrentClause.GetName<T>(expression, alias);
        }

        protected void Append(params string[] tokens)
        {
            CurrentClause.Append(tokens);
        }

        protected void AppendPredicate(string expression, string name, object value)
        {
            Append(expression);
            AppendParameter(name, value);
        }

        protected void AppendCommand(string expression, ICommand command)
        {
            AppendCommand(expression, string.Empty, command, string.Empty, string.Empty);
        }

        protected void AppendCommand(string expression, string prefix, ICommand command, string postfix)
        {
            AppendCommand(expression, prefix, command, postfix, string.Empty);
        }

        protected void AppendCommand(string expression, string prefix, ICommand command, string postfix, string alias)
        {
            CurrentClause.AppendFormat("{0} {1}{2}{3}", expression, prefix, command.Text, postfix);
            if (!string.IsNullOrEmpty(alias))
                CurrentClause.AppendFormat(" {0}", alias);

            foreach (var parameter in command.Parameters)
                AddParameterInternal(parameter.Key, parameter.Value);
        }

        protected void AddParameterInternal(string name, object value)
        {
            _parameters[name] = value;
        }

        protected void AddParameterInternal<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            AddParameterInternal(CurrentClause.GetName<T>(expression), value);
        }

        protected void AddParameterInternal<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            var getter = expression.Compile();
            AddParameterInternal(CurrentClause.GetName<T>(expression), getter(model));
        }

        private static string GetParameterName(string name)
        {
            return string.Format("@{0}", name);
        }

        private void AddParameterIfNew(string name, object value)
        {
            if (_parameters.ContainsKey(name))
                AddParameterInternal(name, value);
        }

        protected void AppendParameter(string name, object value)
        {
            var parameterName = GetParameterName(name);
            AddParameterIfNew(parameterName, value);

            Append(parameterName);
        }

        protected void AppendParameter(string name, object value, string assignment)
        {
            var parameterName = GetParameterName(name);
            AddParameterIfNew(parameterName, value);

            Append(assignment, parameterName);
        }

        protected void SetCallback(Action<IModel, object> callback, IModel model)
        {
            _callback = callback;
            _model = model;
        }

        #endregion

        #region ICommandBuilder Members

        public virtual ICommand ToCommand()
        {
            var command = _factory.Create();

            var text = new StringBuilder();
            foreach (var clause in _clauses)
                text.Append(clause.ToString());

            command.Text = text.ToString();
            command.AddParameters(_parameters);
            command.Callback = _callback;
            command.Model = _model;

            return command;
        }

        #endregion
    }
}
