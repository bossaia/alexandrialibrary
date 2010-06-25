using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IHost
        : INamed
    {
        string Address { get; }
        int Port { get; }
        IEnumerable<IDatabase> Databases { get; }
    }
}
