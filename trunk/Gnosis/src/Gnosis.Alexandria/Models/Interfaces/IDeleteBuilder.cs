using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDeleteBuilder : ICommandBuilder
    {
        IDeleteBuilder Delete { get; }
        IDeleteBuilder OrRollback { get; }
        IDeleteBuilder OrAbort { get; }
        IDeleteBuilder OrReplace { get; }
        IDeleteBuilder OrFail { get; }
        IDeleteBuilder OrIgnore { get; }

        IDeleteBuilder From(string table);

        IDeleteBuilder Where<T>(Expression<Func<T, object>> expression) where T : IModel;
        IDeleteBuilder Where(string expression);
        IDeleteBuilder Or<T>(Expression<Func<T, object>> expression) where T : IModel;
        IDeleteBuilder Or(string expression);
        IDeleteBuilder And<T>(Expression<Func<T, object>> expression) where T : IModel;
        IDeleteBuilder And(string expression);
        IDeleteBuilder OpenParen { get; }
        IDeleteBuilder CloseParen { get; }

        IDeleteBuilder IsEqualTo(string expression);
        IDeleteBuilder IsEqualTo(string name, object value);
        IDeleteBuilder IsEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsGreaterThan(string expression);
        IDeleteBuilder IsGreaterThan(string name, object value);
        IDeleteBuilder IsGreaterThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsGreaterThanOrEqualTo(string expression);
        IDeleteBuilder IsGreaterThanOrEqualTo(string name, object value);
        IDeleteBuilder IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsIn(string expression);
        IDeleteBuilder IsIn(string name, object value);
        IDeleteBuilder IsLessThan(string expression);
        IDeleteBuilder IsLessThan(string name, object value);
        IDeleteBuilder IsLessThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsLessThanOrEqualTo(string expression);
        IDeleteBuilder IsLessThanOrEqualTo(string name, object value);
        IDeleteBuilder IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsLike(string expression);
        IDeleteBuilder IsLike(string name, object value);
        IDeleteBuilder IsLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsNotEqualTo(string expression);
        IDeleteBuilder IsNotEqualTo(string name, object value);
        IDeleteBuilder IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsNotIn(string expression);
        IDeleteBuilder IsNotIn(string name, object value);
        IDeleteBuilder IsNotLike(string expression);
        IDeleteBuilder IsNotLike(string name, object value);
        IDeleteBuilder IsNotLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        IDeleteBuilder IsNotNull { get; }
        IDeleteBuilder IsNull { get; }

        IDeleteBuilder AddParameter(string name, object value);
        IDeleteBuilder AddParameter<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
    }
}
