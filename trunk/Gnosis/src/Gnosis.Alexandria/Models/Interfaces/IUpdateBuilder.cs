using System;
using System.Collections.Generic;
using System.Linq;
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

        IUpdateBuilder Set(string name, object value);

        IUpdateBuilder Where(string expression);
        IUpdateBuilder Or(string expression);
        IUpdateBuilder And(string expression);
        IUpdateBuilder OpenParen { get; }
        IUpdateBuilder CloseParen { get; }

        IUpdateBuilder IsEqualTo(string expression);
        IUpdateBuilder IsEqualTo(string name, object value);
        IUpdateBuilder IsGreaterThan(string expression);
        IUpdateBuilder IsGreaterThan(string name, object value);
        IUpdateBuilder IsGreaterThanOrLessThan(string expression);
        IUpdateBuilder IsGreaterThanOrLessThan(string name, object value);
        IUpdateBuilder IsIn(string expression);
        IUpdateBuilder IsIn(string name, object value);
        IUpdateBuilder IsLessThan(string expression);
        IUpdateBuilder IsLessThan(string name, object value);
        IUpdateBuilder IsLessThanOrEqualTo(string name, object value);
        IUpdateBuilder IsLike(string expression);
        IUpdateBuilder IsLike(string name, object value);
        IUpdateBuilder IsNotEqualTo(string expression);
        IUpdateBuilder IsNotEqualTo(string name, object value);
        IUpdateBuilder IsNotIn(string expression);
        IUpdateBuilder IsNotIn(string name, object value);
        IUpdateBuilder IsNotLike(string expression);
        IUpdateBuilder IsNotLike(string name, object value);
        IUpdateBuilder IsNotNull { get; }
        IUpdateBuilder IsNull { get; }

        IUpdateBuilder AddParameter(string name, object value);
    }
}
