using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface INational : IModel
    {
        ICountry Nationality { get; set; }
    }
}
