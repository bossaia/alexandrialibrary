using System.Data;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IModelMapper<T>
        where T : IModel
    {
        T GetModel(IDataRecord record);
    }
}
