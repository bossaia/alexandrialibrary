using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IStatement
    {
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        string ToString();
    }
}
