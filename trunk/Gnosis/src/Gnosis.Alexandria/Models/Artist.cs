using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Artist : Named, IArtist
    {
        public ICountry Country { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
