using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public enum TriggerType
    {
        AfterDelete,
        AfterInsert,
        AfterUpdate,
        BeforeDelete,
        BeforeInsert,
        BeforeUpdate,
        InsteadOfDelete,
        InsteadOfInsert,
        InsteadOfUpdate
    }
}
