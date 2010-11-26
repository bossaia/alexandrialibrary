using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Country : NamedDated, ICountry
    {
        public Country()
        {
            Code = "XA";
        }

        private Country(object id) : this()
        {
            Initialize(id);
        }

        public string Code { get; set; }

        public static readonly ICountry Unknown = new Country(1L);
    }
}
