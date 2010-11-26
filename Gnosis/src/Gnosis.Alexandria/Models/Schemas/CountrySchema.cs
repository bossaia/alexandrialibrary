using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;

namespace Gnosis.Alexandria.Models.Schemas
{
    public class CountrySchema : SQLiteSchema<ICountry>
    {
        public CountrySchema()
        {
            AddField(x => x.Name, (x, y) => x.Name = y.AsString());
            AddField(x => x.NameHash);
            AddField(x => x.Abbreviation, (x, y) => x.Abbreviation = y.AsString());
            AddField(x => x.Code, (x, y) => x.Code = y.AsString());
            AddField(x => x.FromDate, (x, y) => x.FromDate = y.AsDateTime());
            AddField(x => x.ToDate, (x, y) => x.ToDate = y.AsDateTime());
            AddUniqueKey(Ascending(x => x.Name));
            AddUniqueKey(Ascending(x => x.NameHash));
            AddUniqueKey(Ascending(x => x.Code));
            AddKey(Ascending(x => x.FromDate));
        }
    }
}
