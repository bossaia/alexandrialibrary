using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public interface IColumn
    {
        string Name { get; }
        Type Type { get; }
        object Default { get; }
    }
}
