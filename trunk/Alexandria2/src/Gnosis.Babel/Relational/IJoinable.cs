using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IJoinable
    {
        string Table { get; }
        string Alias { get; }
        IEnumerable<IJoin> Joins { get; }
    }
}
