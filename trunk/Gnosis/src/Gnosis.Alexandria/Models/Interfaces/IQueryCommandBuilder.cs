using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IQueryCommandBuilder
    {
        IQueryCommandBuilder SelectAll { get; }
        IQueryCommandBuilder SelectDistinct { get; }

        IQueryCommandBuilder Column(string expression);
        IQueryCommandBuilder Column(string expression, string alias);

        IQueryCommandBuilder From(string table);
        IQueryCommandBuilder From(string table, string alias);
        IQueryCommandBuilder From(ICommand select);
        IQueryCommandBuilder From(ICommand select, string alias);
        IQueryCommandBuilder CrossJoin(string table, string alias);
        IQueryCommandBuilder InnerJoin(string table, string alias);
        IQueryCommandBuilder LeftOuterJoin(string table, string alias);
        IQueryCommandBuilder On(string expression);

        IQueryCommandBuilder Or(string expression);
        IQueryCommandBuilder And(string expression);
        IQueryCommandBuilder OpenParen { get; }
        IQueryCommandBuilder CloseParen { get; }

        IQueryCommandBuilder GroupBy { get; }
        IQueryCommandBuilder Grouping(string expression);
        IQueryCommandBuilder Having(string expression);

        IQueryCommandBuilder OrderBy { get; }
        IQueryCommandBuilder Ascending(string expression);
        IQueryCommandBuilder Descending(string expression);
        
        IQueryCommandBuilder Limit(int number);
        IQueryCommandBuilder Offset(int number);

        IQueryCommandBuilder Where(string expression);

        IQueryCommandBuilder IsEqualTo(string expression);
        IQueryCommandBuilder IsEqualTo(string name, object value);
        IQueryCommandBuilder IsGreaterThan(string expression);
        IQueryCommandBuilder IsGreaterThan(string name, object value);
        IQueryCommandBuilder IsGreaterThanOrEqualTo(string expression);
        IQueryCommandBuilder IsGreaterThanOrEqualTo(string name, object value);
        IQueryCommandBuilder IsIn(string expression);
        IQueryCommandBuilder IsIn(string name, object value);
        IQueryCommandBuilder IsLessThan(string expression);
        IQueryCommandBuilder IsLessThan(string name, object value);
        IQueryCommandBuilder IsLessThanOrEqualTo(string expression);
        IQueryCommandBuilder IsLessThanOrEqualTo(string name, object value);
        IQueryCommandBuilder IsLike(string expression);
        IQueryCommandBuilder IsLike(string name, object value);
        IQueryCommandBuilder IsNotEqualTo(string expression);
        IQueryCommandBuilder IsNotEqualTo(string name, object value);
        IQueryCommandBuilder IsNotIn(string expression);
        IQueryCommandBuilder IsNotIn(string name, object value);
        IQueryCommandBuilder IsNotLike(string expression);
        IQueryCommandBuilder IsNotLike(string name, object value);
        IQueryCommandBuilder IsNotNull { get; }
        IQueryCommandBuilder IsNull { get; }

        IQueryCommandBuilder Union(ICommand select);
        IQueryCommandBuilder UnionAll(ICommand select);
        IQueryCommandBuilder Intersect(ICommand select);
        IQueryCommandBuilder Except(ICommand select);

        IQueryCommandBuilder AddParameter(string name, object value);

        ICommand ToCommand();
    }
}
