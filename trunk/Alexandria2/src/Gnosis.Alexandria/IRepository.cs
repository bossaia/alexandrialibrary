using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IRepository<T>
        where T : IEntity
    {
        T Get(long id);
        IEnumerable<T> All();
        IEnumerable<T> Search(ISearch<T> search);
        void Initialize();
    }
}
