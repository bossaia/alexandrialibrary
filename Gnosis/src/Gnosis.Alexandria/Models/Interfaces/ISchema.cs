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
        IEnumerable<KeyValuePair<string, IField<T>>> Fields { get; }
        IField<T> GetField(string name);
        IEnumerable<KeyValuePair<string, IKey<T>>> Keys { get; }
    }
}
