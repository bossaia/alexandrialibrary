using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public interface ICreateCommandFactory
    {
        IDbCommand Create(ITable table);
    }
}
