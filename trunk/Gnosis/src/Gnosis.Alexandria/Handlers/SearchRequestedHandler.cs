using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public class SearchRequestedHandler : Handler<ISearchRequestedMessage>
    {
        public SearchRequestedHandler(ArtistRepositoryController parent)
        {
            _parent = parent;
        }

        private readonly ArtistRepositoryController _parent;

        protected override void HandleMessage(ISearchRequestedMessage message)
        {
            var artists = _parent.GetArtistsWithNamesLike(message.Search);
            int count = artists.Count;
        }
    }
}
