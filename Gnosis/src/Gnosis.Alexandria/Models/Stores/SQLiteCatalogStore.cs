using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
