using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class ArtistRepositoryController : Controller
    {
        public ArtistRepositoryController(IDispatcher parent, IArtistRepository repository)
            : base(parent)
        {
            _repository = repository;

            AddHandler(new SearchRequestedHandler(this));
        }

        private readonly IArtistRepository _repository;

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            return _repository.GetArtistsWithNamesLike(search);
        }
    }
}
