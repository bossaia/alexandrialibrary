using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDeleteBuilder : ICommandBuilder
    {
        IDeleteBuilder DeleteFrom(string table);

        IDeleteBuilder OrRollback { get; }
        IDeleteBuilder OrAbort { get; }
        IDeleteBuilder OrReplace { get; }
        IDeleteBuilder OrFail { get; }
        IDeleteBuilder OrIgnore { get; }

        IDeleteBuilder Where(string expression);
        IDeleteBuilder Or(string expression);
        IDeleteBuilder And(string expression);
        IDeleteBuilder OpenParen { get; }
        IDeleteBuilder CloseParen { get; }

        IDeleteBuilder IsEqualTo(string expression);
        IDeleteBuilder IsEqualTo(string name, object value);
        IDeleteBuilder IsGreaterThan(string expression);
        IDeleteBuilder IsGreaterThan(string name, object value);
        IDeleteBuilder IsGreaterThanOrLessThan(string expression);
        IDeleteBuilder IsGreaterThanOrLessThan(string name, object value);
        IDeleteBuilder IsIn(string expression);
        IDeleteBuilder IsIn(string name, object value);
        IDeleteBuilder IsLessThan(string expression);
        IDeleteBuilder IsLessThan(string name, object value);
        IDeleteBuilder IsLessThanOrEqualTo(string name, object value);
        IDeleteBuilder IsLike(string expression);
        IDeleteBuilder IsLike(string name, object value);
        IDeleteBuilder IsNotEqualTo(string expression);
        IDeleteBuilder IsNotEqualTo(string name, object value);
        IDeleteBuilder IsNotIn(string expression);
        IDeleteBuilder IsNotIn(string name, object value);
        IDeleteBuilder IsNotLike(string expression);
        IDeleteBuilder IsNotLike(string name, object value);
        IDeleteBuilder IsNotNull { get; }
        IDeleteBuilder IsNull { get; }

        IDeleteBuilder AddParameter(string name, object value);
    }
}
