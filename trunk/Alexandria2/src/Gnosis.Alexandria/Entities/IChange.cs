using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface IChange
    {
        string EntityName { get; }
        string PropertyName { get; }
        object PropertyValue { get; }
    }
}
