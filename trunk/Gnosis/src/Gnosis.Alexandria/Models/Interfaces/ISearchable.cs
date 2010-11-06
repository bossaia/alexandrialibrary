using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ISearchable : INamed, ICoded, IDated, INational, INoted
    {
        string Type { get; set; }
    }
}
