using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ISchema<T>
        where T : IModel
    {
        string Name { get; }
        IEnumerable<IField<T>> Fields { get; }
        IEnumerable<IField<T>> NonPrimaryFields { get; }
        IField<T> PrimaryField { get; }
        IEnumerable<IKey<T>> Keys { get; }
    }
}
