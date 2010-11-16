using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Artist : Named, IArtist
    {
        public Artist()
        {
            Name = "Unknown";
            Abbreviation = string.Empty;
            Country = Gnosis.Alexandria.Models.Country.Default;
        }

        public ICountry Country { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
