using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface IRepository
    {
        T Create<T>();

        T Lookup<T>(long id)
            where T : IEntity;

        IEnumerable<T> Search<T>(Predicate<T> criteria)
            where T : IEntity;

        void Persist(IChangeSet changeSet);
    }
}
