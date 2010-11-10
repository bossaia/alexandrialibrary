using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;
using Gnosis.Alexandria.Utilities.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class QueryCommandBuilder : IQueryCommandBuilder
    {
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

        public IQueryCommandBuilder SelectAll
        {
            get
            {
                _currentClause = Clause.Select;
                AppendToCurrentClause(Constants.SelectAll);
                return this;
            }
        }

        public IQueryCommandBuilder SelectDistinct
        {
            get
            {
                _currentClause = Clause.Select;
                AppendToCurrentClause(Constants.SelectDistinct);
                return this;
            }
        }

        public IQueryCommandBuilder Column(string expression)
        {
            _currentClause = Clause.Columns;
            AppendToCurrentClause(expression);
            return this;
        }

        public IQueryCommandBuilder Column(string expression, string alias)
        {
            _currentClause = Clause.Columns;
            AppendToCurrentClause(expression, alias);
            return this;
        }

        public IQueryCommandBuilder From(string table)
        {
            _currentClause = Clause.From;
            AppendToCurrentClause(Constants.From, table);
            return this;
        }

        public IQueryCommandBuilder From(string table, string alias)
        {
            _currentClause = Clause.From;
            AppendToCurrentClause(Constants.From, table, alias);
            return this;
        }

        public IQueryCommandBuilder From(ICommand selectCommand)
        {
            _currentClause = Clause.From;
            AppendCommand(_from, Constants.From, Constants.OpenParen, selectCommand, Constants.CloseParen);
            return this;
        }

        public IQueryCommandBuilder From(ICommand selectCommand, string alias)
        {
            _currentClause = Clause.From;
            AppendCommand(_from, Constants.From, Constants.OpenParen, selectCommand, Constants.CloseParen, alias);
            return this;
        }

        public IQueryCommandBuilder CrossJoin(string table, string alias)
        {
            AppendToCurrentClause(Constants.CrossJoin, table, alias);
            return this;
        }

        public IQueryCommandBuilder InnerJoin(string table, string alias)
        {
            AppendToCurrentClause(Constants.InnerJoin, table, alias);
            return this;
        }

        public IQueryCommandBuilder LeftOuterJoin(string table, string alias)
        {
            _from.AppendClause(Constants.LeftOuterJoin, table, alias);
            return this;
        }

        public IQueryCommandBuilder On(string expression)
        {
            AppendToCurrentClause(Constants.On, expression);
            return this;
        }

        public IQueryCommandBuilder Or(string expression)
        {
            AppendToCurrentClause(Constants.Or, expression);
            return this;
        }

        public IQueryCommandBuilder And(string expression)
        {
            AppendToCurrentClause(Constants.And, expression);
            return this;
        }

        public IQueryCommandBuilder OpenParen
        {
            get
            {
                AppendToCurrentClause(Constants.OpenParen);
                return this;
            }
        }

        public IQueryCommandBuilder CloseParen
        {
            get
            {
                AppendToCurrentClause(Constants.CloseParen);
                return this;
            }
        }

        public IQueryCommandBuilder GroupBy
        {
            get
            {
                _currentClause = Clause.GroupBy;
                AppendToCurrentClause(Constants.GroupBy);
                return this;
            }
        }

        public IQueryCommandBuilder Grouping(string expression)
        {
            _currentClause = Clause.Grouping;
            AppendToCurrentClause(expression);
            return this;
        }

        public IQueryCommandBuilder Having(string expression)
        {
            _currentClause = Clause.Grouping;
            AppendToCurrentClause(Constants.Having, expression);
            return this;
        }

        public IQueryCommandBuilder OrderBy
        {
            get 
            {
                _currentClause = Clause.OrderBy;
                AppendToCurrentClause(Constants.OrderBy);
                return this;
            }
        }

        public IQueryCommandBuilder Ascending(string expression)
        {
            _currentClause = Clause.Ordering;
            AppendToCurrentClause(expression, Constants.Ascending);
            return this;
        }

        public IQueryCommandBuilder Descending(string expression)
        {
            _currentClause = Clause.Ordering;
            AppendToCurrentClause(expression, Constants.Descending);
            return this;
        }

        public IQueryCommandBuilder Limit(int number)
        {
            _currentClause = Clause.Limit;
            AppendToCurrentClause(Constants.Limit, number.ToString());
            return this;
        }

        public IQueryCommandBuilder Offset(int number)
        {
            _currentClause = Clause.Limit;
            AppendToCurrentClause(Constants.Offset, number.ToString());
            return this;
        }

        public IQueryCommandBuilder Where(string expression)
        {
            _currentClause = Clause.Where;
            AppendToCurrentClause(Constants.Where, expression);
            return this;
        }

        public IQueryCommandBuilder IsEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsEqualTo, expression);
            return this;
        }

        public IQueryCommandBuilder IsEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsEqualTo, name, value);
            return this;
        }

        public IQueryCommandBuilder IsGreaterThan(string expression)
        {
            AppendToCurrentClause(Constants.IsGreaterThan, expression);
            return this;
        }

        public IQueryCommandBuilder IsGreaterThan(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThan, name, value);
            return this;
        }

        public IQueryCommandBuilder IsGreaterThanOrEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsGreaterThanOrEqualTo, expression);
            return this;
        }

        public IQueryCommandBuilder IsGreaterThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThanOrEqualTo, name, value);
            return this;
        }

        public IQueryCommandBuilder IsIn(string expression)
        {
            AppendToCurrentClause(Constants.IsIn, expression);
            return this;
        }

        public IQueryCommandBuilder IsIn(string name, object value)
        {
            AppendToCurrentClause(Constants.IsIn, Constants.OpenParen);
            AppendParameter(name, value);
            AppendToCurrentClause(Constants.CloseParen);
            return this;
        }

        public IQueryCommandBuilder IsLessThan(string expression)
        {
            AppendToCurrentClause(Constants.IsLessThan, expression);
            return this;
        }

        public IQueryCommandBuilder IsLessThan(string name, object value)
        {
            AppendPredicate(Constants.IsLessThan, name, value);
            return this;
        }

        public IQueryCommandBuilder IsLessThanOrEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsLessThanOrEqualTo, expression);
            return this;
        }

        public IQueryCommandBuilder IsLessThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsLessThanOrEqualTo, name, value);
            return this;
        }

        public IQueryCommandBuilder IsLike(string expression)
        {
            AppendToCurrentClause(Constants.IsLike, expression);
            return this;
        }

        public IQueryCommandBuilder IsLike(string name, object value)
        {
            AppendPredicate(Constants.IsLike, name, value);
            return this;
        }

        public IQueryCommandBuilder IsNotEqualTo(string expression)
        {
            AppendToCurrentClause(Constants.IsNotEqualTo, expression);
            return this;
        }

        public IQueryCommandBuilder IsNotEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsNotEqualTo, name, value);
            return this;
        }

        public IQueryCommandBuilder IsNotIn(string expression)
        {
            AppendToCurrentClause(Constants.IsNotIn, Constants.OpenParen, expression, Constants.CloseParen);
            return this;
        }

        public IQueryCommandBuilder IsNotIn(string name, object value)
        {
            AppendToCurrentClause(Constants.IsNotIn, Constants.OpenParen);
            AppendParameter(name, value);
            AppendToCurrentClause(Constants.CloseParen);
            return this;
        }

        public IQueryCommandBuilder IsNotLike(string expression)
        {
            AppendToCurrentClause(Constants.IsNotLike, expression);
            return this;
        }

        public IQueryCommandBuilder IsNotLike(string name, object value)
        {
            AppendPredicate(Constants.IsNotLike, name, value);
            return this;
        }

        public IQueryCommandBuilder IsNotNull
        {
            get
            {
                AppendToCurrentClause(Constants.IsNotNull);
                return this;
            }
        }

        public IQueryCommandBuilder IsNull
        {
            get
            {
                AppendToCurrentClause(Constants.IsNull);
                return this;
            }
        }

        public IQueryCommandBuilder Union(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.Union, select);
            return this;
        }

        public IQueryCommandBuilder UnionAll(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.UnionAll, select);
            return this;
        }

        public IQueryCommandBuilder Intersect(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.Intersect, select);
            return this;
        }

        public IQueryCommandBuilder Except(ICommand select)
        {
            _currentClause = Clause.Compound;
            AppendCommand(_compound, Constants.Except, select);
            return this;
        }

        public IQueryCommandBuilder AddParameter(string name, object value)
        {
            _parameters[name] = value;
            return this;
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
