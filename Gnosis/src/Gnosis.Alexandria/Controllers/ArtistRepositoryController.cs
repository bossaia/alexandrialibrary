using System.Collections.Generic;

using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class ArtistRepositoryController : Controller
    {
        public ArtistRepositoryController(IArtistRepository repository)
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
