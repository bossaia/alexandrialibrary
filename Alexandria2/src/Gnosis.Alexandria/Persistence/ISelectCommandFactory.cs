using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public interface ISelectCommandFactory
    {
        IDbCommand Create(ITable table);
        IDbCommand Create(ITable table, ITuple criteria);
    }
}
