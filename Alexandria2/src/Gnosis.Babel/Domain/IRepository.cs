using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Domain
{
    public interface IRepository
    {
        T Create<T>()
            where T : IEntity;

        T Lookup<T>(object id)
            where T : IEntity;

        IEnumerable<T> Search<T>(Predicate<T> criteria)
            where T : IEntity;

        void Save<T>(ISet<T> entities)
            where T : IEntity;
    }
}
