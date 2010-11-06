using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IModelMapper<T>
        where T : IModel
    {
        void Map(T model, IDataRecord record);
    }
}
