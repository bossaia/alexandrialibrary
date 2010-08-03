using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IInsertable
    {
        string GetInsertStatement(ITuple tuple);
    }
}
