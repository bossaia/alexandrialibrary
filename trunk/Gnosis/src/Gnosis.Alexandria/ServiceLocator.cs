using System;

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
using Gnosis.Alexandria.Models.Schemas;
using Gnosis.Alexandria.Models.Stores;
using Gnosis.Alexandria.Views;
using Gnosis.Alexandria.Views.Interfaces;
using StructureMap;

namespace Gnosis.Alexandria
{
    public static class ServiceLocator
    {
        static ServiceLocator()
        {
            try {
            ObjectFactory.Initialize(x =>
                {
                    x.For<IFactory<ICommand>>().Use<GenericFactory<ICommand, Command>>();

                    x.For<IFactory<ICountry>>().Use<GenericFactory<ICountry, Country>>();
                    x.For<IFactory<IArtist>>().Use<GenericFactory<IArtist, Artist>>();

                    x.For<IFactory<IInsertBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<IUpdateBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<IDeleteBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<ISelectBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<ICreateTableBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<ICreateIndexBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<ICreateViewBuilder>>().Use<CommandBuilderFactory>();
                    x.For<IFactory<ICreateTriggerBuilder>>().Use<CommandBuilderFactory>();

                    x.For<IModelMapper<IArtist>>().Use<ModelMapper<IArtist>>();
                    x.For<IModelMapper<ICountry>>().Use<ModelMapper<ICountry>>();
                    x.For<IPersistMapper<ICountry>>().Use<PersistMapper<ICountry>>();
                    x.For<IPersistMapper<IArtist>>().Use<PersistMapper<IArtist>>();
                    x.For<IQueryMapper<ICountry>>().Use<QueryMapper<ICountry>>();
                    x.For<IQueryMapper<IArtist>>().Use<QueryMapper<IArtist>>();
                    x.For<ISchemaMapper<ICountry>>().Use<SchemaMapper<ICountry>>();
                    x.For<ISchemaMapper<IArtist>>().Use<SchemaMapper<IArtist>>();
                    x.For<ISchema<ICountry>>().Use<CountrySchema>();
                    x.For<ISchema<IArtist>>().Use<ArtistSchema>();
                    x.For<IStore>().Use<SQLiteCatalogStore>();
                    x.For<ICountryRepository>().Use<CountryRepository>();
                    x.For<IArtistRepository>().Use<ArtistRepository>();
                    
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
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        public static T GetObject<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
