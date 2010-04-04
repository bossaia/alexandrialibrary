using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface IFactory<T>
        where T : IEntity
    {
        T CreateInstance();
        T CreateInstance(IMap<string, object> state);
    }
}
