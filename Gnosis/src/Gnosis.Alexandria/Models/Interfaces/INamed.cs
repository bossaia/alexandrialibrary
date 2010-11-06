using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface INamed : IModel
    {
        string Name { get; set; }
        string Abbreviation { get; set; }
        string NameHash { get; }
    }
}
