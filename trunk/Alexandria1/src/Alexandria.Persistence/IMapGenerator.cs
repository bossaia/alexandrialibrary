using System.Xml;

namespace Telesophy.Alexandria.Persistence
{
    public interface IMapGenerator
    {
        string FileName { get; }
        XmlDocument Generate();
    }
}
