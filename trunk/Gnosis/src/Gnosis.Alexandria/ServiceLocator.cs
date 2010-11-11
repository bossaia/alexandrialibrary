using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Commands;
using Gnosis.Alexandria.Models.Factories;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Models.Mappers;
using Gnosis.Alexandria.Models.Repositories;
using Gnosis.Alexandria.Views;
using Gnosis.Alexandria.Views.Interfaces;
using StructureMap;

namespace Gnosis.Alexandria
{
    public static class ServiceLocator
    {
        static ServiceLocator()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.For<IFactory<ICommand>>().Use<GenericFactory<ICommand, Command>>();
                    //x.For<IFactory<ICommandBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<ICountry>>().Use<GenericFactory<ICountry, Country>>();
                    x.For<IModelMapper<ICountry>>().Use<CountryModelMapper>();
                    //x.For<ICommandMapper<ICountry>>().Use<CountryCommandMapper>();
                    //x.For<ICountryRepository>().Use<CountryRepository>();
                    x.For<IFactory<IArtist>>().Use<GenericFactory<IArtist, Artist>>();
                    x.For<IModelMapper<IArtist>>().Use<ArtistModelMapper>();
                    //x.For<ICommandMapper<IArtist>>().Use<ArtistCommandMapper>();
                    //x.For<IArtistRepository>().Use<ArtistRepository>();
                    
                    x.For<IRepositoryController>().Use<RepositoryController>();
                    x.For<IArtistRepositoryController>().Use<ArtistRepositoryController>();
                    x.For<ICountryRepositoryController>().Use<CountryRepositoryController>();
                    x.For<ITabController>().Use<TabController>();
                    
                    x.For<IHomeTabView>().Use<HomeTabView>();
                    x.For<ISearchTabView>().Use<SearchTabView>();

                    x.For<INewHomeTabRequestedHandler>().Use<NewHomeTabRequestedHandler>();
                    x.For<INewSearchTabRequestedHandler>().Use<NewSearchTabRequestedHandler>();
                    x.For<ISearchRequestedHandler>().Use<SearchRequestedHandler>();

                    x.For<INewHomeTabRequestedMessage>().Use<NewHomeTabRequestedMessage>();
                    x.For<INewSearchTabRequestedMessage>().Use<NewSearchTabRequestedMessage>();
                    x.For<ISearchRequestedMessage>().Use<SearchRequestedMessage>();
                }
            );
        }

        public static T GetObject<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
