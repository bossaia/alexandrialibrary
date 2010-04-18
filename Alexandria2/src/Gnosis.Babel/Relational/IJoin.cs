using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IJoin
    {
        ISourceExpression Parent { get; }
        JoinType Type { get; }
        IExpression Expression { get; }
        ISourceExpression Child { get; }
    }
}
