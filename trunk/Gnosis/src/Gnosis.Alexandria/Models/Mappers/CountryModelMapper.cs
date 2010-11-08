using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class CountryModelMapper : ModelMapper<ICountry>
    {
        public CountryModelMapper()
        {
            AddAction("Name", (x, y) => x.Name = GetString(y));
            AddAction("Abbreviation", (x, y) => x.Abbreviation = GetString(y));
            AddAction("Code", (x, y) => x.Code = GetString(y));
        }
    }
}
