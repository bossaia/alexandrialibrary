using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IInsertBuilder : ICommandBuilder
    {
        IInsertBuilder Insert(string table);

        IInsertBuilder OrRollback { get; }
        IInsertBuilder OrAbort { get; }
        IInsertBuilder OrReplace { get; }
        IInsertBuilder OrFail { get; }
        IInsertBuilder OrIgnore { get; }

        IInsertBuilder ColumnAndValue(string name, object value);

        IInsertBuilder AddParameter(string name, object value);
    }
}
