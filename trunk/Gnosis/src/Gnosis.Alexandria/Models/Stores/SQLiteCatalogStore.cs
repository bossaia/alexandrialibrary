using Gnosis.Babel.SQLite;

namespace Gnosis.Alexandria.Models.Stores
{
    public class SQLiteCatalogStore : SQLiteStore
    {
        public SQLiteCatalogStore()
            : base("Catalog")
        {
        }
    }
}
