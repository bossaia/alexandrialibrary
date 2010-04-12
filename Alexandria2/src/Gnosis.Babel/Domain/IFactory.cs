using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Domain
{
    public interface IFactory<T>
        where T : IEntity
    {
        T Create();
        T Create(IMap<string, object> state);
    }
}
