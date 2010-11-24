using System.Collections.Generic;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class ArtistRepositoryController : Controller, IArtistRepositoryController
    {
        public ArtistRepositoryController(IArtistRepository repository, ISearchRequestedHandler searchRequestedHandler, IInitializeRepositoriesHandler initializeRepositoriesHandler)
        {
            _repository = repository;
            searchRequestedHandler.Controller = this;
            initializeRepositoriesHandler.Controller = this;
            
            AddHandler(searchRequestedHandler);
            AddHandler(initializeRepositoriesHandler);
        }

        private readonly IArtistRepository _repository;

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            return _repository.GetArtistsWithNamesLike(search);
        }

        public void Initialize()
        {
            _repository.Initialize();
        }
    }
}
