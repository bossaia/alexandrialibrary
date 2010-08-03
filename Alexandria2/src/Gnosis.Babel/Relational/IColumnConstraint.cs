using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IColumnConstraint :
        IColumnObject,
        IAddable,
        IRemovable,
        INamed,
        IRenameable
    {
    }
}
