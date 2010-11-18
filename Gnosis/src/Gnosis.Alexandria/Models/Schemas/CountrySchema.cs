using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Schemas
{
    public class CountrySchema : Schema<ICountry>
    {
        public CountrySchema()
            : base("Country")
        {
            AddField(x => x.Name, (x, y) => x.Name = y.AsString());
            AddField(x => x.NameHash);
            AddField(x => x.Abbreviation, (x, y) => x.Abbreviation = y.AsString());
            AddField(x => x.Code, (x, y) => x.Code = y.AsString());
            AddUniqueKey(Ascending(x => x.Name));
            AddUniqueKey(Ascending(x => x.NameHash));
            AddUniqueKey(Ascending(x => x.Code));
        }
    }
}
