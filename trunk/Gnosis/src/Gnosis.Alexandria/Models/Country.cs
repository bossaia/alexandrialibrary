using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Country : Named, ICountry
    {
        public Country()
        {
            Name = "Unknown";
            Abbreviation = string.Empty;
            Code = "XA";
        }

        private Country(object id) : this()
        {
            Initialize(id);
        }

        public string Code { get; set; }

        public static readonly ICountry Default = new Country(1L);
    }
}
