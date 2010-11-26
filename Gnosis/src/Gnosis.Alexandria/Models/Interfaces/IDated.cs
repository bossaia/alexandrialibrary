using System;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDated : IModel
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}
