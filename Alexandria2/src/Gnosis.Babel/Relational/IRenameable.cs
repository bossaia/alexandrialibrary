using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IRenameable
    {
        string GetRenameClause(string renameTo);
        string GetRenameStatement(string renameTo);
    }
}
