using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface IState
    {
        long Id { get; }
        IMap<string, object> Data { get; }
    }
}
