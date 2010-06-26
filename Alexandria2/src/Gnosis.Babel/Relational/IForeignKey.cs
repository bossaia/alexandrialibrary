using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IForeignKey
        : IConstraint
    {
        ITable ReferenceTable { get; }
        IEnumerable<string> GetSourceColumns();
        IEnumerable<string> GetReferenceColumns();
        ReferenceOption OnUpdate { get; }
        ReferenceOption OnDelete { get; }
        string Match { get; }
        DeferralOption Deferred { get; }
    }
}
