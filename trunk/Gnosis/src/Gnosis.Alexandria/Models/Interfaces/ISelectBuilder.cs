using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ISelectBuilder
    {
        ISelectBuilder SelectAll { get; }
        ISelectBuilder SelectDistinct { get; }

        ISelectBuilder Column(string expression);
        ISelectBuilder Column(string expression, string alias);

        ISelectBuilder From(string table);
        ISelectBuilder From(string table, string alias);
        ISelectBuilder From(ICommand select);
        ISelectBuilder From(ICommand select, string alias);
        ISelectBuilder CrossJoin(string table, string alias);
        ISelectBuilder InnerJoin(string table, string alias);
        ISelectBuilder LeftOuterJoin(string table, string alias);
        ISelectBuilder On(string expression);

        ISelectBuilder Or(string expression);
        ISelectBuilder And(string expression);
        ISelectBuilder OpenParen { get; }
        ISelectBuilder CloseParen { get; }

        ISelectBuilder GroupBy { get; }
        ISelectBuilder Grouping(string expression);
        ISelectBuilder Having(string expression);

        ISelectBuilder OrderBy { get; }
        ISelectBuilder Ascending(string expression);
        ISelectBuilder Descending(string expression);
        
        ISelectBuilder Limit(int number);
        ISelectBuilder Offset(int number);

        ISelectBuilder Where(string expression);

        ISelectBuilder IsEqualTo(string expression);
        ISelectBuilder IsEqualTo(string name, object value);
        ISelectBuilder IsGreaterThan(string expression);
        ISelectBuilder IsGreaterThan(string name, object value);
        ISelectBuilder IsGreaterThanOrEqualTo(string expression);
        ISelectBuilder IsGreaterThanOrEqualTo(string name, object value);
        ISelectBuilder IsIn(string expression);
        ISelectBuilder IsIn(string name, object value);
        ISelectBuilder IsLessThan(string expression);
        ISelectBuilder IsLessThan(string name, object value);
        ISelectBuilder IsLessThanOrEqualTo(string expression);
        ISelectBuilder IsLessThanOrEqualTo(string name, object value);
        ISelectBuilder IsLike(string expression);
        ISelectBuilder IsLike(string name, object value);
        ISelectBuilder IsNotEqualTo(string expression);
        ISelectBuilder IsNotEqualTo(string name, object value);
        ISelectBuilder IsNotIn(string expression);
        ISelectBuilder IsNotIn(string name, object value);
        ISelectBuilder IsNotLike(string expression);
        ISelectBuilder IsNotLike(string name, object value);
        ISelectBuilder IsNotNull { get; }
        ISelectBuilder IsNull { get; }

        ISelectBuilder Union(ICommand select);
        ISelectBuilder UnionAll(ICommand select);
        ISelectBuilder Intersect(ICommand select);
        ISelectBuilder Except(ICommand select);

        ISelectBuilder AddParameter(string name, object value);

        ICommand ToCommand();
    }
}
