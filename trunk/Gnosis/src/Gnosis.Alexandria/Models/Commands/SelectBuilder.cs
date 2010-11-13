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
    public class SelectBuilder : ISelectBuilder
    {
        public SelectBuilder()
        {
        }

        #region Inner Classes

        private enum Clause
        {
            Select = 0,
            Columns,
            From,
            Where,
            GroupBy,
            Grouping,
            OrderBy,
            Ordering,
            Limit,
            Compound
        }

        private static class Constants
        {
            public const string AllColumns = "*";
            public const string And = "and";
            public const string Ascending = "asc";
            public const string CloseParen = ")";
            public const string CrossJoin = "cross join";
            public const string Descending = "desc";
            public const string Except = " except ";
            public const string From = " from";
            public const string GroupBy = " group by ";
            public const string Having = "having";
            public const string InnerJoin = "inner join";
            public const string Intersect = " intersect ";
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
            public const string OrderBy = " order by ";
            public const string SelectAll = "select all ";
            public const string SelectDistinct = "select distinct ";
            public const string Space = " ";
            public const string StatementTerminator = ";";
            public const string Union = " union ";
            public const string UnionAll = " union all ";
            public const string Where = " where";
        }

        #endregion

        #region Private Members

        private readonly FluentStringBuilder _select = new FluentStringBuilder();
        private readonly FluentStringBuilder _columns = new FluentStringBuilder();
        private readonly FluentStringBuilder _from = new FluentStringBuilder(Constants.Space, Constants.Space);
        private readonly FluentStringBuilder _where = new FluentStringBuilder(Constants.Space, Constants.Space);
        private readonly FluentStringBuilder _groupBy = new FluentStringBuilder();
        private readonly FluentStringBuilder _grouping = new FluentStringBuilder();
        private readonly FluentStringBuilder _orderBy = new FluentStringBuilder();
        private readonly FluentStringBuilder _ordering = new FluentStringBuilder();
        private readonly FluentStringBuilder _limit = new FluentStringBuilder(Constants.Space, Constants.Space);
        private readonly FluentStringBuilder _compound = new FluentStringBuilder(Constants.Space, Constants.Space);
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();
        
        private Clause _currentClause = Clause.Select;

        private string GetCommandText()
        {
            return new FluentStringBuilder()
                .Append(_select)
                .Append(_columns)
                .Append(_from)
                .Append(_where)
                .Append(_groupBy)
                .Append(_grouping)
                .Append(_compound)
                .Append(_orderBy)
                .Append(_ordering)
                .Append(_limit)
                .Append(Constants.StatementTerminator)
            .ToString();
        }

        private void AppendToCurrentClause(params string[] tokens)
        {
            switch (_currentClause)
            {
                case Clause.Select:
                    _select.AppendClause(tokens);
                    break;
                case Clause.Columns:
                    _columns.AppendClause(tokens);
                    break;
                case Clause.From:
                    _from.AppendClause(tokens);
                    break;
                case Clause.Where:
                    _where.AppendClause(tokens);
                    break;
                case Clause.GroupBy:
                    _groupBy.AppendClause(tokens);
                    break;
                case Clause.Grouping:
                    _grouping.AppendClause(tokens);
                    break;
                case Clause.Compound:
                    _compound.AppendClause(tokens);
                    break;
                case Clause.OrderBy:
                    _orderBy.AppendClause(tokens);
                    break;
                case Clause.Ordering:
                    _ordering.AppendClause(tokens);
                    break;
                case Clause.Limit:
                    _limit.AppendClause(tokens);
                    break;
                default:
                    break;
            }
        }

        private void AppendPredicate(string expression, string name, object value)
        {
            AppendToCurrentClause(expression);
            AppendParameter(name, value);
        }

        private void AppendParameter(string name, object value)
        {
            AddParameter(name, value);
            AppendToCurrentClause(name);
        }

        private void AppendCommand(IStringBuilder builder, string expression, ICommand command)
        {
            AppendCommand(builder, expression, string.Empty, command, string.Empty, string.Empty);
        }

        private void AppendCommand(IStringBuilder builder, string expression, string prefix, ICommand command, string postfix)
        {
            AppendCommand(builder, expression, prefix, command, postfix, string.Empty);
        }

        private void AppendCommand(IStringBuilder builder, string expression, string prefix, ICommand command, string postfix, string alias)
        {
            builder.AppendFormat("{0} {1}{2}{3}", expression, prefix, command.Text, postfix);
            if (!string.IsNullOrEmpty(alias))
                builder.AppendFormat(" {0}", alias);

            foreach (var parameter in command.Parameters)
                AddParameter(parameter.Key, parameter.Value);
        }

        private static string GetName<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return expression.ToName();
        }

        private static string GetName<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return string.Format("{0}.{1}", alias, GetName(expression));
        }

        #endregion

        public ISelectBuilder SelectAll
        {
            get
            {
                _currentClause = Clause.Select;
                AppendToCurrentClause(Constants.SelectAll);
                return this;
            }
        }

        public ISelectBuilder SelectDistinct
        {
            get
            {
                _currentClause = Clause.Select;
                AppendToCurrentClause(Constants.SelectDistinct);
                return this;
            }
        }

        public ISelectBuilder Column(string expression)
        {
            _currentClause = Clause.Columns;
            AppendToCurrentClause(expression);
            return this;
        }

        public ISelectBuilder Column(string expression, string alias)
        {
            _currentClause = Clause.Columns;
            AppendToCurrentClause(expression, alias);
            return this;
        }

        public ISelectBuilder AllColumns
        {
            get
            {
                _currentClause = Clause.Columns;
                AppendToCurrentClause(Constants.AllColumns);
                return this;
            }
        }

        public ISelectBuilder From(string table)
        {
            _currentClause = Clause.From;
            AppendToCurrentClause(Constants.From, table);
            return this;
        }

        public ISelectBuilder From(string table, string alias)
        {
            _currentClause = Clause.From;
            AppendToCurrentClause(Constants.From, table, alias);
            return this;
        }

        public ISelectBuilder From(ICommand selectCommand)
        {
            _currentClause = Clause.From;
            AppendCommand(_from, Constants.From, Constants.OpenParen, selectCommand, Constants.CloseParen);
            return this;
        }

        public ISelectBuilder From(ICommand selectCommand, string alias)
        {
            _currentClause = Clause.From;
            AppendCommand(_from, Constants.From, Constants.OpenParen, selectCommand, Constants.CloseParen, alias);
            return this;
        }

        public ISelectBuilder CrossJoin(string table, string alias)
        {
            AppendToCurrentClause(Constants.CrossJoin, table, alias);
            return this;
        }

        public ISelectBuilder InnerJoin(string table, string alias)
        {
            AppendToCurrentClause(Constants.InnerJoin, table, alias);
            return this;
        }

        public ISelectBuilder LeftOuterJoin(string table, string alias)
        {
            _from.AppendClause(Constants.LeftOuterJoin, table, alias);
            return this;
        }

        public ISelectBuilder On(string expression)
        {
            AppendToCurrentClause(Constants.On, expression);
            return this;
        }

        public ISelectBuilder On<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return On(GetName(expression));
        }

        public ISelectBuilder On<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return On(GetName(expression, alias));
        }

        public ISelectBuilder Or<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Or(GetName(expression));
        }

        public ISelectBuilder Or<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Or(GetName(expression, alias));
        }

        public ISelectBuilder Or(string expression)
        {
            AppendToCurrentClause(Constants.Or, expression);
            return this;
        }

        public ISelectBuilder And<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return And(GetName(expression));
        }

        public ISelectBuilder And<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return And(GetName(expression, alias));
        }

        public ISelectBuilder And(string expression)
        {
            AppendToCurrentClause(Constants.And, expression);
            return this;
        }

        public ISelectBuilder OpenParen
        {
            get
            {
                AppendToCurrentClause(Constants.OpenParen);
                return this;
            }
        }

        public ISelectBuilder CloseParen
        {
            get
            {
                AppendToCurrentClause(Constants.CloseParen);
                return this;
            }
        }

        public ISelectBuilder GroupBy
        {
            get
            {
                _currentClause = Clause.GroupBy;
                AppendToCurrentClause(Constants.GroupBy);
                return this;
            }
        }

        public ISelectBuilder Grouping(string expression)
        {
            _currentClause = Clause.Grouping;
            AppendToCurrentClause(expression);
            return this;
        }

        public ISelectBuilder Grouping<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Grouping(GetName(expression));
        }

        public ISelectBuilder Grouping<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Grouping(GetName(expression, alias));
        }

        public ISelectBuilder Having(string expression)
        {
            _currentClause = Clause.Grouping;
            AppendToCurrentClause(Constants.Having, expression);
            return this;
        }

        public ISelectBuilder Having<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Having(GetName(expression));
        }

        public ISelectBuilder Having<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Having(GetName(expression, alias));
        }

        public ISelectBuilder OrderBy
        {
            get 
            {
                _currentClause = Clause.OrderBy;
                AppendToCurrentClause(Constants.OrderBy);
                return this;
            }
        }

        public ISelectBuilder Ascending(string expression)
        {
            _currentClause = Clause.Ordering;
            AppendToCurrentClause(expression, Constants.Ascending);
            return this;
        }

        public ISelectBuilder Ascending<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Ascending(GetName(expression));
        }

        public ISelectBuilder Ascending<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Ascending(GetName(expression, alias));
        }

        public ISelectBuilder Descending(string expression)
        {
            _currentClause = Clause.Ordering;
            AppendToCurrentClause(expression, Constants.Descending);
            return this;
        }

        public ISelectBuilder Descending<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Descending(GetName(expression));
        }

        public ISelectBuilder Descending<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Descending(GetName(expression, alias));
        }

        public ISelectBuilder Limit(int number)
        {
            _currentClause = Clause.Limit;
            AppendToCurrentClause(Constants.Limit, number.ToString());
            return this;
        }

        public ISelectBuilder Offset(int number)
        {
            _currentClause = Clause.Limit;
            AppendToCurrentClause(Constants.Offset, number.ToString());
            return this;
        }

        public ISelectBuilder Where(string expression)
        {
            _currentClause = Clause.Where;
            AppendToCurrentClause(Constants.Where, expression);
            return this;
        }

        public ISelectBuilder Where<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Where(GetName(expression));
        }

        public ISelectBuilder Where<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Where(GetName(expression, alias));
        }

        public ISelectBuilder IsEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsEqualTo, expression);
            return this;
        }

        public ISelectBuilder IsEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsEqualTo, name, value);
            return this;
        }

        public ISelectBuilder IsEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsEqualTo(GetName(expression), value);
        }

        public ISelectBuilder IsGreaterThan(string expression)
        {
            AppendToCurrentClause(Constants.IsGreaterThan, expression);
            return this;
        }

        public ISelectBuilder IsGreaterThan(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThan, name, value);
            return this;
        }

        public ISelectBuilder IsGreaterThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsGreaterThan(GetName(expression), value);
        }

        public ISelectBuilder IsGreaterThanOrEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsGreaterThanOrEqualTo, expression);
            return this;
        }

        public ISelectBuilder IsGreaterThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThanOrEqualTo, name, value);
            return this;
        }

        public ISelectBuilder IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsGreaterThanOrEqualTo(GetName(expression), value);
        }

        public ISelectBuilder IsIn(string expression)
        {
            AppendToCurrentClause(Constants.IsIn, expression);
            return this;
        }

        public ISelectBuilder IsIn(string name, object value)
        {
            AppendToCurrentClause(Constants.IsIn, Constants.OpenParen);
            AppendParameter(name, value);
            AppendToCurrentClause(Constants.CloseParen);
            return this;
        }

        public ISelectBuilder IsLessThan(string expression)
        {
            AppendToCurrentClause(Constants.IsLessThan, expression);
            return this;
        }

        public ISelectBuilder IsLessThan(string name, object value)
        {
            AppendPredicate(Constants.IsLessThan, name, value);
            return this;
        }

        public ISelectBuilder IsLessThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLessThan(GetName(expression), value);
        }

        public ISelectBuilder IsLessThanOrEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsLessThanOrEqualTo, expression);
            return this;
        }

        public ISelectBuilder IsLessThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsLessThanOrEqualTo, name, value);
            return this;
        }


        public ISelectBuilder IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLessThanOrEqualTo(GetName(expression), value);
        }

        public ISelectBuilder IsLike(string expression)
        {
            AppendToCurrentClause(Constants.IsLike, expression);
            return this;
        }

        public ISelectBuilder IsLike(string name, object value)
        {
            AppendPredicate(Constants.IsLike, name, value);
            return this;
        }

        public ISelectBuilder IsLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLike(GetName(expression), value);
        }

        public ISelectBuilder IsNotEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsNotEqualTo, expression);
            return this;
        }

        public ISelectBuilder IsNotEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsNotEqualTo, name, value);
            return this;
        }


        public ISelectBuilder IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsNotEqualTo(GetName(expression), value);
        }

        public ISelectBuilder IsNotIn(string expression)
        {
            AppendToCurrentClause(Constants.IsNotIn, Constants.OpenParen, expression, Constants.CloseParen);
            return this;
        }

        public ISelectBuilder IsNotIn(string name, object value)
        {
            AppendToCurrentClause(Constants.IsNotIn, Constants.OpenParen);
            AppendParameter(name, value);
            AppendToCurrentClause(Constants.CloseParen);
            return this;
        }

        public ISelectBuilder IsNotLike(string expression)
        {
            AppendToCurrentClause(Constants.IsNotLike, expression);
            return this;
        }

        public ISelectBuilder IsNotLike(string name, object value)
        {
            AppendPredicate(Constants.IsNotLike, name, value);
            return this;
        }

        public ISelectBuilder IsNotLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsNotLike(GetName(expression), value);
        }

        public ISelectBuilder IsNotNull
        {
            get
            {
                AppendToCurrentClause(Constants.IsNotNull);
                return this;
            }
        }

        public ISelectBuilder IsNull
        {
            get
            {
                AppendToCurrentClause(Constants.IsNull);
                return this;
            }
        }

        public ISelectBuilder Union(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.Union, select);
            return this;
        }

        public ISelectBuilder UnionAll(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.UnionAll, select);
            return this;
        }

        public ISelectBuilder Intersect(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.Intersect, select);
            return this;
        }

        public ISelectBuilder Except(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.Except, select);
            return this;
        }

        public ISelectBuilder AddParameter(string name, object value)
        {
            _parameters[name] = value;
            return this;
        }

        public ISelectBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return AddParameter(GetName(expression), value);
        }

        public ICommand ToCommand()
        {
            var command = new Command
            {
                Text = GetCommandText()
            };
            command.AddParameters(_parameters);

            return command;
        }
    }
}
