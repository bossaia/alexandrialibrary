using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class DeleteBuilder : CommandBuilder, IDeleteBuilder
    {
        public DeleteBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
            AddSpaceDelimitedClause(Clauses.Delete);
            AddSpaceDelimitedClause(Clauses.From);
            AddSpaceDelimitedClause(Clauses.Where);
        }

        public IDeleteBuilder Delete
        {
            get
            {
                SetCurrentClause(Clauses.Delete);
                Append(Constants.Delete);
                return this;
            }
        }

        public IDeleteBuilder OrAbort
        {
            get
            {
                Append(Constants.OrAbort);
                return this;
            }
        }

        public IDeleteBuilder OrFail
        {
            get
            {
                Append(Constants.OrFail);
                return this;
            }
        }

        public IDeleteBuilder OrIgnore 
        {
            get
            {
                Append(Constants.OrIgnore);
                return this;
            }
        }

        public IDeleteBuilder OrReplace
        {
            get
            {
                Append(Constants.OrReplace);
                return this;
            }
        }

        public IDeleteBuilder OrRollback
        {
            get
            {
                Append(Constants.OrRollback);
                return this;
            }
        }

        public IDeleteBuilder From(string table)
        {
            SetCurrentClause(Clauses.From);
            Append(Constants.From, table);
            return this;
        }

        public IDeleteBuilder Where(string expression)
        {
            SetCurrentClause(Clauses.Where);
            Append(Constants.Where, expression);
            return this;
        }

        public IDeleteBuilder Where<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return Where(GetName(expression));
        }

        public IDeleteBuilder Where<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Where(GetName(expression, alias));
        }

        public IDeleteBuilder Or<T>(Expression<Func<T, object>> expression)
    where T : IModel
        {
            return Or(GetName(expression));
        }

        public IDeleteBuilder Or<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return Or(GetName(expression, alias));
        }

        public IDeleteBuilder Or(string expression)
        {
            Append(Constants.Or, expression);
            return this;
        }

        public IDeleteBuilder And<T>(Expression<Func<T, object>> expression)
            where T : IModel
        {
            return And(GetName(expression));
        }

        public IDeleteBuilder And<T>(Expression<Func<T, object>> expression, string alias)
            where T : IModel
        {
            return And(GetName(expression, alias));
        }

        public IDeleteBuilder And(string expression)
        {
            Append(Constants.And, expression);
            return this;
        }

        public IDeleteBuilder OpenParen
        {
            get
            {
                Append(Constants.OpenParen);
                return this;
            }
        }

        public IDeleteBuilder CloseParen
        {
            get
            {
                Append(Constants.CloseParen);
                return this;
            }
        }

        public IDeleteBuilder IsEqualTo(string expression)
        {
            Append(Constants.IsEqualTo, expression);
            return this;
        }

        public IDeleteBuilder IsEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsEqualTo, name, value);
            return this;
        }

        public IDeleteBuilder IsEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsEqualTo(GetName(expression), value);
        }

        public IDeleteBuilder IsGreaterThan(string expression)
        {
            Append(Constants.IsGreaterThan, expression);
            return this;
        }

        public IDeleteBuilder IsGreaterThan(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThan, name, value);
            return this;
        }

        public IDeleteBuilder IsGreaterThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsGreaterThan(GetName(expression), value);
        }

        public IDeleteBuilder IsGreaterThanOrEqualTo(string expression)
        {
            Append(Constants.IsGreaterThanOrEqualTo, expression);
            return this;
        }

        public IDeleteBuilder IsGreaterThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsGreaterThanOrEqualTo, name, value);
            return this;
        }

        public IDeleteBuilder IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsGreaterThanOrEqualTo(GetName(expression), value);
        }

        public IDeleteBuilder IsIn(string expression)
        {
            Append(Constants.IsIn, expression);
            return this;
        }

        public IDeleteBuilder IsIn(string name, object value)
        {
            Append(Constants.IsIn, Constants.OpenParen);
            AppendParameter(name, value);
            Append(Constants.CloseParen);
            return this;
        }

        public IDeleteBuilder IsLessThan(string expression)
        {
            Append(Constants.IsLessThan, expression);
            return this;
        }

        public IDeleteBuilder IsLessThan(string name, object value)
        {
            AppendPredicate(Constants.IsLessThan, name, value);
            return this;
        }

        public IDeleteBuilder IsLessThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLessThan(GetName(expression), value);
        }

        public IDeleteBuilder IsLessThanOrEqualTo(string expression)
        {
            Append(Constants.IsLessThanOrEqualTo, expression);
            return this;
        }

        public IDeleteBuilder IsLessThanOrEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsLessThanOrEqualTo, name, value);
            return this;
        }

        public IDeleteBuilder IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLessThanOrEqualTo(GetName(expression), value);
        }

        public IDeleteBuilder IsLike(string expression)
        {
            Append(Constants.IsLike, expression);
            return this;
        }

        public IDeleteBuilder IsLike(string name, object value)
        {
            AppendPredicate(Constants.IsLike, name, value);
            return this;
        }

        public IDeleteBuilder IsLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsLike(GetName(expression), value);
        }

        public IDeleteBuilder IsNotEqualTo(string expression)
        {
            Append(Constants.IsNotEqualTo, expression);
            return this;
        }

        public IDeleteBuilder IsNotEqualTo(string name, object value)
        {
            AppendPredicate(Constants.IsNotEqualTo, name, value);
            return this;
        }


        public IDeleteBuilder IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsNotEqualTo(GetName(expression), value);
        }

        public IDeleteBuilder IsNotIn(string expression)
        {
            Append(Constants.IsNotIn, Constants.OpenParen, expression, Constants.CloseParen);
            return this;
        }

        public IDeleteBuilder IsNotIn(string name, object value)
        {
            Append(Constants.IsNotIn, Constants.OpenParen);
            AppendParameter(name, value);
            Append(Constants.CloseParen);
            return this;
        }

        public IDeleteBuilder IsNotLike(string expression)
        {
            Append(Constants.IsNotLike, expression);
            return this;
        }

        public IDeleteBuilder IsNotLike(string name, object value)
        {
            AppendPredicate(Constants.IsNotLike, name, value);
            return this;
        }

        public IDeleteBuilder IsNotLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            return IsNotLike(GetName(expression), value);
        }

        public IDeleteBuilder IsNotNull
        {
            get
            {
                Append(Constants.IsNotNull);
                return this;
            }
        }

        public IDeleteBuilder IsNull
        {
            get
            {
                Append(Constants.IsNull);
                return this;
            }
        }

        public IDeleteBuilder AddParameter(string name, object value)
        {
            AddParameter(name, value);
            return this;
        }

        public IDeleteBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            AddParameter(expression, value);
            return this;
        }

        public IDeleteBuilder AddParameter<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            AddParameter(expression, model);
            return this;
        }
    }
}
