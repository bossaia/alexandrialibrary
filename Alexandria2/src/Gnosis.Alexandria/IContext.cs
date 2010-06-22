using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IContext
    {
        T Create<T>() where T : IEntity;
        T Get<T>(long id) where T : IEntity;
        IEnumerable<T> All<T>() where T : IEntity;
        IEnumerable<T> Search<T>(ISearch<T> search) where T : IEntity;

        void Initialize();
        void Persist(IChange change);
    }
}
