using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IDeleted
    {
        string GetDeletedSql(IEnumerable<KeyValuePair<string, object>> deleted);
    }
}
