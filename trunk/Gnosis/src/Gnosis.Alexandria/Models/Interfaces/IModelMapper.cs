using System.Data;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IModelMapper<T>
        where T : IModel
    {
        void Map(T model, IDataRecord record);
    }
}
