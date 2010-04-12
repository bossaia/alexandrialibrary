using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.Domain;

namespace Gnosis.Alexandria.Entities
{
    public interface IPlaylist :
        IEntity,
        INamed,
        ITagged,
        ILinked
    {
        IEnumerable<ISelection> Selections { get; }
        void AddSelection(ISelection selection);
        void RemoveSelection(ISelection selection);
    }
}
