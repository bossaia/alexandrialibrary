using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Commands
{
    public class UpdateBuilder : CommandBuilder, IUpdateBuilder
    {
        public UpdateBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
            AddSpaceDelimitedClause(Clauses.Update);
            AddSpaceDelimitedClause(Clauses.Table);
            AddSpaceDelimitedClause(Clauses.Set);
            AddCommaDelimitedClause(Clauses.ColumnAssignments);
            AddSpaceDelimitedClause(Clauses.Where);
        }

        public IUpdateBuilder Update
        {
            get
            {
                SetCurrentClause(Clauses.Update);
                Append(Constants.Update);
                return this;
            }
        }

        public IUpdateBuilder OrAbort
        {
            get
            {
                Append(Constants.OrAbort);
                return this;
            }
        }

        public IUpdateBuilder OrFail
        {
            get
            {
                Append(Constants.OrFail);
                return this;
            }
        }

        public IUpdateBuilder OrIgnore 
        {
            get
            {
                Append(Constants.OrIgnore);
                return this;
            }
        }

        public IUpdateBuilder OrReplace
        {
            get
            {
                Append(Constants.OrReplace);
                return this;
            }
        }

        public IUpdateBuilder OrRollback
        {
            get
            {
                Append(Constants.OrRollback);
                return this;
            }
        }

        public IUpdateBuilder Set
        {
            get
            {
                SetCurrentClause(Clauses.Set);
                Append(Constants.Set);
                return this;
            }
        }

        public IUpdateBuilder Table(string table)
        {
            SetCurrentClause(Clauses.From);
            Append(" " + table);
            return this;
        }

        public IUpdateBuilder ColumnToValue<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            var getter = expression.Compile();
            ColumnToValue(GetName(expression), getter(model)); 
            return this;
        }

        public IUpdateBuilder ColumnToValue(string name, object value)
        {
            SetCurrentClause(Clauses.ColumnAssignments);
            AppendParameter(name, value, Constants.ColumnAssignment);
            return this;
        }

        public IUpdateBuilder ColumnsToValues<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel
        {
            expressions.Each<Expression<Func<T, object>>>(x => ColumnToValue(x, model));
            return this;
        }

        public IUpdateBuilder Where(string expression)
        {
            SetCurrentClause(Clauses.Where);
            Append(Constants.Where, expression);
            return this;
        }

        public IUpdateBuilder Where<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Where(GetName(expression));
        }

        public IUpdateBuilder Where<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Where(GetName(expression, alias));
        }

        public IUpdateBuilder Or<T>(Expression<Func<T, object>> expression)
    where T : IModel
        {
            return Or(GetName(expression));
        }

        public IUpdateBuilder Or<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Or(GetName(expression, alias));
        }

        public IUpdateBuilder Or(string expression)
        {
            Append(Constants.Or, expression);
            return this;
        }

        public IUpdateBuilder And<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return And(GetName(expression));
        }

        public IUpdateBuilder And<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return And(GetName(expression, alias));
        }

        public IUpdateBuilder And(string expression)
        {
            Append(Constants.And, expression);
            return this;
        }

        public IUpdateBuilder OpenParen
        {
            get
            {
                Append(Constants.OpenParen);
                return this;
            }
        }

        public IUpdateBuilder CloseParen
        {
            get
            {
                Append(Constants.CloseParen);
                return this;
            }
        }

        public IUpdateBuilder IsEqualTo(string expression)
        {
            Append(Constants.IsEqualTo, expression);
            return this;
        }

        public IUpdateBuilder IsEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsEqualTo, name, value);
            return this;
        }

        public IUpdateBuilder IsEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsEqualTo(GetName(expression), value);
        }

        public IUpdateBuilder IsGreaterThan(string expression)
        {
            Append(Constants.IsGreaterThan, expression);
            return this;
        }

        public IUpdateBuilder IsGreaterThan(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThan, name, value);
            return this;
        }

        public IUpdateBuilder IsGreaterThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsGreaterThan(GetName(expression), value);
        }

        public IUpdateBuilder IsGreaterThanOrEqualTo(string expression)
        {
            Append(Constants.IsGreaterThanOrEqualTo, expression);
            return this;
        }

        public IUpdateBuilder IsGreaterThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThanOrEqualTo, name, value);
            return this;
        }

        public IUpdateBuilder IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsGreaterThanOrEqualTo(GetName(expression), value);
        }

        public IUpdateBuilder IsIn(string expression)
        {
            Append(Constants.IsIn, expression);
            return this;
        }

        public IUpdateBuilder IsIn(string name, object value)
        {
            Append(Constants.IsIn, Constants.OpenParen);
            AppendParameter(name, value);
            Append(Constants.CloseParen);
            return this;
        }

        public IUpdateBuilder IsLessThan(string expression)
        {
            Append(Constants.IsLessThan, expression);
            return this;
        }

        public IUpdateBuilder IsLessThan(string name, object value)
        {
            AppendPredicate(Constants.IsLessThan, name, value);
            return this;
        }

        public IUpdateBuilder IsLessThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLessThan(GetName(expression), value);
        }

        public IUpdateBuilder IsLessThanOrEqualTo(string expression)
        {
            Append(Constants.IsLessThanOrEqualTo, expression);
            return this;
        }

        public IUpdateBuilder IsLessThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsLessThanOrEqualTo, name, value);
            return this;
        }

        public IUpdateBuilder IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLessThanOrEqualTo(GetName(expression), value);
        }

        public IUpdateBuilder IsLike(string expression)
        {
            Append(Constants.IsLike, expression);
            return this;
        }

        public IUpdateBuilder IsLike(string name, object value)
        {
            AppendPredicate(Constants.IsLike, name, value);
            return this;
        }

        public IUpdateBuilder IsLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLike(GetName(expression), value);
        }

        public IUpdateBuilder IsNotEqualTo(string expression)
        {
            Append(Constants.IsNotEqualTo, expression);
            return this;
        }

        public IUpdateBuilder IsNotEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsNotEqualTo, name, value);
            return this;
        }


        public IUpdateBuilder IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsNotEqualTo(GetName(expression), value);
        }

        public IUpdateBuilder IsNotIn(string expression)
        {
            Append(Constants.IsNotIn, Constants.OpenParen, expression, Constants.CloseParen);
            return this;
        }

        public IUpdateBuilder IsNotIn(string name, object value)
        {
            Append(Constants.IsNotIn, Constants.OpenParen);
            AppendParameter(name, value);
            Append(Constants.CloseParen);
            return this;
        }

        public IUpdateBuilder IsNotLike(string expression)
        {
            Append(Constants.IsNotLike, expression);
            return this;
        }

        public IUpdateBuilder IsNotLike(string name, object value)
        {
            AppendPredicate(Constants.IsNotLike, name, value);
            return this;
        }

        public IUpdateBuilder IsNotLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsNotLike(GetName(expression), value);
        }

        public IUpdateBuilder IsNotNull
        {
            get
            {
                Append(Constants.IsNotNull);
                return this;
            }
        }

        public IUpdateBuilder IsNull
        {
            get
            {
                Append(Constants.IsNull);
                return this;
            }
        }

        public IUpdateBuilder AddParameter(string name, object value)
        {
            AddParameter(name, value);
            return this;
        }

        public IUpdateBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            AddParameter(expression, value);
            return this;
        }

        public IUpdateBuilder AddParameter<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            AddParameter(expression, model);
            return this;
        }
    }
}
