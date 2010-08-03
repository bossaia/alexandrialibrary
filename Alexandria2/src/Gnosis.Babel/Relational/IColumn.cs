using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IColumn :
        ITableObject,
        IAddable,
        IRemovable,
        INamed,
        IRenameable
    {
        IDomain Domain { get; }
        string GetChangeDomain(IDomain domain);
    }
}
