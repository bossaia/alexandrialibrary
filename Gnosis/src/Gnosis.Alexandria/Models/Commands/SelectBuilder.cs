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
    public class SelectBuilder : CommandBuilder, ISelectBuilder
    {
        public SelectBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
            AddCommaDelimitedClause(Clauses.Select);
            AddCommaDelimitedClause(Clauses.Columns);
            AddSpaceDelimitedClause(Clauses.From);
            AddSpaceDelimitedClause(Clauses.Where);
            AddSpaceDelimitedClause(Clauses.GroupBy);
            AddCommaDelimitedClause(Clauses.Groupings);
            AddSpaceDelimitedClause(Clauses.OrderBy);
            AddCommaDelimitedClause(Clauses.Orderings);
            AddSpaceDelimitedClause(Clauses.Limit);
            AddSpaceDelimitedClause(Clauses.Compound);
        }

        #region ISelectBuilder Members

        public ISelectBuilder SelectAll
        {
            get
            {
                SetCurrentClause(Clauses.Select);
                Append(Constants.SelectAll);
                return this;
            }
        }

        public ISelectBuilder SelectDistinct
        {
            get
            {
                SetCurrentClause(Clauses.Select);
                Append(Constants.SelectDistinct);
                return this;
            }
        }

        public ISelectBuilder Column(string expression)
        {
            SetCurrentClause(Clauses.Columns);
            Append(expression);
            return this;
        }

        public ISelectBuilder Column(string expression, string alias)
        {
            SetCurrentClause(Clauses.Columns);
            Append(expression, alias);
            return this;
        }

        public ISelectBuilder AllColumns
        {
            get
            {
                SetCurrentClause(Clauses.Columns);
                Append(Constants.AllColumns);
                return this;
            }
        }

        public ISelectBuilder From(string table)
        {
            SetCurrentClause(Clauses.From);
            Append(Constants.From, table);
            return this;
        }

        public ISelectBuilder From(string table, string alias)
        {
            SetCurrentClause(Clauses.From);
            Append(Constants.From, table, alias);
            return this;
        }

        public ISelectBuilder From(ICommand selectCommand)
        {
            SetCurrentClause(Clauses.From);
            AppendCommand(Constants.From, Constants.OpenParen, selectCommand, Constants.CloseParen);
            return this;
        }

        public ISelectBuilder From(ICommand selectCommand, string alias)
        {
            SetCurrentClause(Clauses.From);
            AppendCommand(Constants.From, Constants.OpenParen, selectCommand, Constants.CloseParen, alias);
            return this;
        }

        public ISelectBuilder CrossJoin(string table, string alias)
        {
            Append(Constants.CrossJoin, table, alias);
            return this;
        }

        public ISelectBuilder InnerJoin(string table, string alias)
        {
            Append(Constants.InnerJoin, table, alias);
            return this;
        }

        public ISelectBuilder LeftOuterJoin(string table, string alias)
        {
            Append(Constants.LeftOuterJoin, table, alias);
            return this;
        }

        public ISelectBuilder On(string expression)
        {
            Append(Constants.On, expression);
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
            Append(Constants.Or, expression);
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
            Append(Constants.And, expression);
            return this;
        }

        public ISelectBuilder OpenParen
        {
            get
            {
                Append(Constants.OpenParen);
                return this;
            }
        }

        public ISelectBuilder CloseParen
        {
            get
            {
                Append(Constants.CloseParen);
                return this;
            }
        }

        public ISelectBuilder GroupBy
        {
            get
            {
                SetCurrentClause(Clauses.GroupBy);
                Append(Constants.GroupBy);
                return this;
            }
        }

        public ISelectBuilder Grouping(string expression)
        {
            SetCurrentClause(Clauses.Groupings);
            Append(expression);
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
            SetCurrentClause(Clauses.Groupings);
            Append(Constants.Having, expression);
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
                SetCurrentClause(Clauses.OrderBy);
                Append(Constants.OrderBy);
                return this;
            }
        }

        public ISelectBuilder Ascending(string expression)
        {
            SetCurrentClause(Clauses.Orderings);
            Append(expression, Constants.Ascending);
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
            SetCurrentClause(Clauses.Orderings);
            Append(expression, Constants.Descending);
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
            SetCurrentClause(Clauses.Limit);
            Append(Constants.Limit, number.ToString());
            return this;
        }

        public ISelectBuilder Offset(int number)
        {
            SetCurrentClause(Clauses.Limit);
            Append(Constants.Offset, number.ToString());
            return this;
        }

        public ISelectBuilder Where(string expression)
        {
            SetCurrentClause(Clauses.Where);
            Append(Constants.Where, expression);
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
            Append(Constants.IsEqualTo, expression);
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
            Append(Constants.IsGreaterThan, expression);
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
            Append(Constants.IsGreaterThanOrEqualTo, expression);
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
            Append(Constants.IsIn, expression);
            return this;
        }

        public ISelectBuilder IsIn(string name, object value)
        {
            Append(Constants.IsIn, Constants.OpenParen);
            AppendParameter(name, value);
            Append(Constants.CloseParen);
            return this;
        }

        public ISelectBuilder IsLessThan(string expression)
        {
            Append(Constants.IsLessThan, expression);
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
            Append(Constants.IsLessThanOrEqualTo, expression);
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
            Append(Constants.IsLike, expression);
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
            Append(Constants.IsNotEqualTo, expression);
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
            Append(Constants.IsNotIn, Constants.OpenParen, expression, Constants.CloseParen);
            return this;
        }

        public ISelectBuilder IsNotIn(string name, object value)
        {
            Append(Constants.IsNotIn, Constants.OpenParen);
            AppendParameter(name, value);
            Append(Constants.CloseParen);
            return this;
        }

        public ISelectBuilder IsNotLike(string expression)
        {
            Append(Constants.IsNotLike, expression);
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
                Append(Constants.IsNotNull);
                return this;
            }
        }

        public ISelectBuilder IsNull
        {
            get
            {
                Append(Constants.IsNull);
                return this;
            }
        }

        public ISelectBuilder Union(ICommand select)
        {
            SetCurrentClause(Clauses.Compound);
            AppendCommand(Constants.Union, select);
            return this;
        }

        public ISelectBuilder UnionAll(ICommand select)
        {
            SetCurrentClause(Clauses.Compound);
            AppendCommand(Constants.UnionAll, select);
            return this;
        }

        public ISelectBuilder Intersect(ICommand select)
        {
            SetCurrentClause(Clauses.Compound);
            AppendCommand(Constants.Intersect, select);
            return this;
        }

        public ISelectBuilder Except(ICommand select)
        {
            SetCurrentClause(Clauses.Compound);
            AppendCommand(Constants.Except, select);
            return this;
        }

        public ISelectBuilder AddParameter(string name, object value)
        {
            AddParameter(name, value);
            return this;
        }

        public ISelectBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            AddParameter(expression, value);
            return this;
        }

        #endregion
    }
}
