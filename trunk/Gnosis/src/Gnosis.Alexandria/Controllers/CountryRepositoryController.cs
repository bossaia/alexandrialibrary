using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class CountryRepositoryController : Controller, ICountryRepositoryController
    {
        public CountryRepositoryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        private readonly ICountryRepository _countryRepository;
    }
}
