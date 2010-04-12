using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Domain
{
    public interface IEntity
    {
        object Id { get; }

        bool IsChanged { get; }
        bool IsNew { get; }


    }
}
