using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IUpdateBuilder : ICommandBuilder
    {
        IUpdateBuilder Update(string table);

        IUpdateBuilder OrRollback { get; }
        IUpdateBuilder OrAbort { get; }
        IUpdateBuilder OrReplace { get; }
        IUpdateBuilder OrFail { get; }
        IUpdateBuilder OrIgnore { get; }

        IUpdateBuilder Set<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder Set(string name, object value);
        IUpdateBuilder SetAll<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel;

        IUpdateBuilder Where<T>(Expression<Func<T, object>> expression) where T : IModel;
        IUpdateBuilder Where(string expression);
        IUpdateBuilder Or<T>(Expression<Func<T, object>> expression) where T : IModel;
        IUpdateBuilder Or(string expression);
        IUpdateBuilder And<T>(Expression<Func<T, object>> expression) where T : IModel;
        IUpdateBuilder And(string expression);
        IUpdateBuilder OpenParen { get; }
        IUpdateBuilder CloseParen { get; }

        IUpdateBuilder IsEqualTo(string expression);
        IUpdateBuilder IsEqualTo(string name, object value);
        IUpdateBuilder IsEqualTo<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsGreaterThan(string expression);
        IUpdateBuilder IsGreaterThan(string name, object value);
        IUpdateBuilder IsGreaterThan<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsGreaterThanOrLessThan(string expression);
        IUpdateBuilder IsGreaterThanOrLessThan(string name, object value);
        IUpdateBuilder IsGreaterThanOrLessThan<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsIn(string expression);
        IUpdateBuilder IsIn(string name, object value);
        IUpdateBuilder IsIn<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsLessThan(string expression);
        IUpdateBuilder IsLessThan(string name, object value);
        IUpdateBuilder IsLessThan<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsLessThanOrEqualTo(string expression);
        IUpdateBuilder IsLessThanOrEqualTo(string name, object value);
        IUpdateBuilder IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsLike(string expression);
        IUpdateBuilder IsLike(string name, object value);
        IUpdateBuilder IsLike<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsNotEqualTo(string expression);
        IUpdateBuilder IsNotEqualTo(string name, object value);
        IUpdateBuilder IsNotEqualTo<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsNotIn(string expression);
        IUpdateBuilder IsNotIn(string name, object value);
        IUpdateBuilder IsNotIn<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsNotLike(string expression);
        IUpdateBuilder IsNotLike(string name, object value);
        IUpdateBuilder IsNotLike<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IUpdateBuilder IsNotNull { get; }
        IUpdateBuilder IsNull { get; }

        IUpdateBuilder AddParameter(string name, object value);
        IUpdateBuilder AddParameter<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
    }
}
