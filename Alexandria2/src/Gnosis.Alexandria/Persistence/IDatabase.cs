using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public interface IDatabase
    {
        string Name { get; }
        IDbCommand GetCommand(string text);
        IDbConnection GetConnection();
    }
}
