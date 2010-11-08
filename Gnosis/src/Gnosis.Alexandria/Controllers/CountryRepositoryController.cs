using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class CountryRepositoryController : Controller
    {
        public CountryRepositoryController(IDispatcher parent, ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        private readonly ICountryRepository _countryRepository;
    }
}
