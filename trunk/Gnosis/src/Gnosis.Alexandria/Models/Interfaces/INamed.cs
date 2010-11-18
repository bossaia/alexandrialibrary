using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface INamed : IModel
    {
        string Name { get; set; }
        string Abbreviation { get; set; }
        string NameHash { get; }
    }
}
