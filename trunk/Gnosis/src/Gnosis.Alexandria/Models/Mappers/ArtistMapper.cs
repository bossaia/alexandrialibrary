using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class ArtistMapper : ModelMapper<IArtist>
    {
        public ArtistMapper(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;

            AddAction("Name", (x, y) => x.Name = GetString(y));
            AddAction("Abbreviation", (x, y) => x.Abbreviation = GetString(y));
            AddAction("Country", (x, y) => x.Country = _countryRepository.GetOne(y));
            AddAction("Date", (x, y) => x.Date = GetDateTime(y));
            AddAction("Note", (x, y) => x.Note = GetString(y));
        }

        private readonly ICountryRepository _countryRepository;
    }
}
