using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Factories
{
    public class GenericFactory<T, C> : IFactory<T>
        where C : T, new()
    {
        public T Create()
        {
            return new C();
        }
    }
}
