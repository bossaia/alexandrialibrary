using System.Data;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IModelMapper<T>
        where T : IModel
    {
        object GetId(IDataRecord record);
        T GetModel(IDataRecord record);
    }
}
