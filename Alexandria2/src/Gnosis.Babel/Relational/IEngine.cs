using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IEngine
    {
        IDbTransaction GetTransaction();
        IDbCommand GetCreateTableCommand(ITable table);
        IDbCommand GetSelectCommand(IQuery query);
        IDbCommand GetChangeCommand(ICommand command);
    }
}
