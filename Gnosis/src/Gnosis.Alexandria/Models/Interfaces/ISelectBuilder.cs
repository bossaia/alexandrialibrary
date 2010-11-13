using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ISelectBuilder
    {
        ISelectBuilder SelectAll { get; }
        ISelectBuilder SelectDistinct { get; }

        ISelectBuilder Column(string expression);
        ISelectBuilder Column(string expression, string alias);
        ISelectBuilder AllColumns { get; }

        ISelectBuilder From(string table);
        ISelectBuilder From(string table, string alias);
        ISelectBuilder From(ICommand select);
        ISelectBuilder From(ICommand select, string alias);
        ISelectBuilder CrossJoin(string table, string alias);
        ISelectBuilder InnerJoin(string table, string alias);
        ISelectBuilder LeftOuterJoin(string table, string alias);
        ISelectBuilder On(string expression);
        ISelectBuilder On<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder On<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;

        ISelectBuilder GroupBy { get; }
        ISelectBuilder Grouping(string expression);
        ISelectBuilder Grouping<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder Grouping<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        ISelectBuilder Having(string expression);
        ISelectBuilder Having<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder Having<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;

        ISelectBuilder OrderBy { get; }
        ISelectBuilder Ascending(string expression);
        ISelectBuilder Ascending<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder Ascending<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        ISelectBuilder Descending(string expression);
        ISelectBuilder Descending<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder Descending<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        
        ISelectBuilder Limit(int number);
        ISelectBuilder Offset(int number);

        ISelectBuilder Where<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder Where<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        ISelectBuilder Where(string expression);
        ISelectBuilder Or<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder Or<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        ISelectBuilder Or(string expression);
        ISelectBuilder And<T>(Expression<Func<T, object>> expression) where T : IModel;
        ISelectBuilder And<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        ISelectBuilder And(string expression);
        ISelectBuilder OpenParen { get; }
        ISelectBuilder CloseParen { get; }

        ISelectBuilder IsEqualTo(string expression);
        ISelectBuilder IsEqualTo(string name, object value);
        ISelectBuilder IsEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsGreaterThan(string expression);
        ISelectBuilder IsGreaterThan(string name, object value);
        ISelectBuilder IsGreaterThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsGreaterThanOrEqualTo(string expression);
        ISelectBuilder IsGreaterThanOrEqualTo(string name, object value);
        ISelectBuilder IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsIn(string expression);
        ISelectBuilder IsIn(string name, object value);
        ISelectBuilder IsLessThan(string expression);
        ISelectBuilder IsLessThan(string name, object value);
        ISelectBuilder IsLessThan<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsLessThanOrEqualTo(string expression);
        ISelectBuilder IsLessThanOrEqualTo(string name, object value);
        ISelectBuilder IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsLike(string expression);
        ISelectBuilder IsLike(string name, object value);
        ISelectBuilder IsLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsNotEqualTo(string expression);
        ISelectBuilder IsNotEqualTo(string name, object value);
        ISelectBuilder IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsNotIn(string expression);
        ISelectBuilder IsNotIn(string name, object value);
        ISelectBuilder IsNotLike(string expression);
        ISelectBuilder IsNotLike(string name, object value);
        ISelectBuilder IsNotLike<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ISelectBuilder IsNotNull { get; }
        ISelectBuilder IsNull { get; }

        ISelectBuilder AddParameter(string name, object value);
        ISelectBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel;
        ICommand ToCommand();
    }
}
