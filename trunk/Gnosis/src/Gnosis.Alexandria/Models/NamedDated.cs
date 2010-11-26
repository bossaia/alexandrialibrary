using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public abstract class NamedDated : Named, IDated
    {
        protected NamedDated()
        {
            FromDate = DateTime.MinValue;
            ToDate = DateTime.MaxValue;
        }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
