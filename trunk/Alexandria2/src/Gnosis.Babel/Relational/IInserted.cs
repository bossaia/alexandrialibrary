using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IInserted
    {
        string GetInsertSql(IEnumerable<KeyValuePair<string, object>> inserted);
    }
}
