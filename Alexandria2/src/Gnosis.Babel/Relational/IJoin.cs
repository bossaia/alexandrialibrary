using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IJoin
    {
        string SourceTable { get; }
        string SourceAlias { get; }
        string ReferenceTable { get; }
        string ReferenceAlias { get; }
        JoinType Type { get; }
        string Expression { get; }
    }
}
