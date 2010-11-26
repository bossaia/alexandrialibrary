using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface INumbered : IModel
    {
        int Number { get; set; }
    }
}
