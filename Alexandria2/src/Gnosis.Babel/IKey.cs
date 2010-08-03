using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IKey :
        INamed
    {
        KeyType Type { get; }
        IEnumerable<string> Elements { get; }
    }
}
