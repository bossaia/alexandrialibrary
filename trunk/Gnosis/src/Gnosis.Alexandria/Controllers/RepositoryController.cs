using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class RepositoryController : Controller, IRepositoryController
    {
        public RepositoryController(IArtistRepository artistRepository, ICountryRepository countryRepository)
        {
            AddChild(new ArtistRepositoryController(artistRepository));
            AddChild(new CountryRepositoryController(countryRepository));
        }
    }
}
