using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Schemas
{
    public class ArtistSchema : Schema<IArtist>
    {
        public ArtistSchema()
            : base("Artist")
        {
            AddField(x => x.Name, (x,y) => x.Name = y.AsString());
            AddField(x => x.NameHash);
            AddField(x => x.Abbreviation, (x,y) => x.Abbreviation = y.AsString());
            AddField(x => x.Country, (x,y) => x.Country = y.AsCountry());
            AddField(x => x.Date, (x,y) => x.Date = y.AsDateTime());
            AddField(x=> x.Note, (x,y) => x.Note = y.AsString());
            AddUniqueKey(Ascending(x => x.Country), Ascending(x => x.Name), Ascending(x => x.Date));
            AddKey(Ascending(x => x.Name));
            AddKey(Ascending(x => x.NameHash));
        }
    }
}
