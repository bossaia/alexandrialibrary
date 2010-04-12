using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.Domain;

namespace Gnosis.Alexandria.Entities
{
    public interface IArtist :
        IEntity,
        INamed,
        ITagged
    {
        DateTime StartDate { get; }
        IEnumerable<IAlbum> Albums { get; }

        void ChangeStartDate(DateTime startDate);
        void AddAlbum(IAlbum album);
        void RemoveAlbum(IAlbum album);
    }
}
