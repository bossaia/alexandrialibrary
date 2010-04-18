using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ISourceExpression
    {
        IDatabase Database { get; }
        ISource Source { get; }
        string Alias { get; }
    }
}
