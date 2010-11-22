using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyColumn : Statement, IForeignKeyColumn
    {
        public IForeignKeyReferenceConstraint References(string table)
        {
            throw new NotImplementedException();
        }

        public IForeignKeyColumn Column(string name)
        {
            throw new NotImplementedException();
        }
    }
}
