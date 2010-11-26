using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public abstract class NamedDatedNational : NamedDated, INational
    {
        protected NamedDatedNational()
        {
            Nationality = Country.Unknown;
        }

        public ICountry Nationality { get; set; }
    }
}
