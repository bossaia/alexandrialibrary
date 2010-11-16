using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICache<T>
        where T : IModel
    {
        bool IsEmpty { get; }
        bool IsFull { get; }
        T GetOne(object id);
        ICollection<T> GetAll();
        void Put(T model);
    }
}
