using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Country : Named, ICountry
    {
        public string Code { get; set; }
    }
}
