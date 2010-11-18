using System;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDated : IModel
    {
        DateTime Date { get; set; }
    }
}
