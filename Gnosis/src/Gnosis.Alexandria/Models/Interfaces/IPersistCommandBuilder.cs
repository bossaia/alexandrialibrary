using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IPersistCommandBuilder
    {
        IPersistCommandBuilder Insert(string table);
        IPersistCommandBuilder Update(string table);
        IPersistCommandBuilder Delete(string table);

        IPersistCommandBuilder OrRollback { get; }
        IPersistCommandBuilder OrAbort { get; }
        IPersistCommandBuilder OrReplace { get; }
        IPersistCommandBuilder OrFail { get; }
        IPersistCommandBuilder OrIgnore { get; }

        IPersistCommandBuilder Set(string name, object value);

        IPersistCommandBuilder Where(string expression);
        IPersistCommandBuilder Or(string expression);
        IPersistCommandBuilder And(string expression);
        IPersistCommandBuilder OpenParen { get; }
        IPersistCommandBuilder CloseParen { get; }

        IPersistCommandBuilder IsEqualTo(string expression);
        IPersistCommandBuilder IsEqualTo(string name, object value);
        IPersistCommandBuilder IsGreaterThan(string expression);
        IPersistCommandBuilder IsGreaterThan(string name, object value);
        IPersistCommandBuilder IsGreaterThanOrLessThan(string expression);
        IPersistCommandBuilder IsGreaterThanOrLessThan(string name, object value);
        IPersistCommandBuilder IsIn(string expression);
        IPersistCommandBuilder IsIn(string name, object value);
        IPersistCommandBuilder IsLessThan(string expression);
        IPersistCommandBuilder IsLessThan(string name, object value);
        IPersistCommandBuilder IsLessThanOrEqualTo(string name, object value);
        IPersistCommandBuilder IsLike(string expression);
        IPersistCommandBuilder IsLike(string name, object value);
        IPersistCommandBuilder IsNotEqualTo(string expression);
        IPersistCommandBuilder IsNotEqualTo(string name, object value);
        IPersistCommandBuilder IsNotIn(string expression);
        IPersistCommandBuilder IsNotIn(string name, object value);
        IPersistCommandBuilder IsNotLike(string expression);
        IPersistCommandBuilder IsNotLike(string name, object value);
        IPersistCommandBuilder IsNotNull { get; }
        IPersistCommandBuilder IsNull { get; }

        IPersistCommandBuilder AddParameter(string name, object value);

        ICommand ToCommand();
    }
}
