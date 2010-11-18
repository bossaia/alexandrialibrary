using System;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Handlers.Interfaces;
using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Models.Repositories;
using Gnosis.Alexandria.Models.Schemas;
using Gnosis.Alexandria.Views;
using Gnosis.Alexandria.Views.Interfaces;

using Gnosis.Babel;
using Gnosis.Babel.SQLite;

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
                    //x.For<IFactory<ICommand>>().Use<GenericFactory<ICommand, Command>>();
                    //x.For<IFactory<ICountry>>().Use<GenericFactory<ICountry, Country>>();
                    //x.For<IFactory<IArtist>>().Use<GenericFactory<IArtist, Artist>>();

                    //x.For<IFactory<IInsertBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<IUpdateBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<IDeleteBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<ISelectBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<ICreateTableBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<ICreateIndexBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<ICreateViewBuilder>>().Use<CommandBuilderFactory>();
                    //x.For<IFactory<ICreateTriggerBuilder>>().Use<CommandBuilderFactory>();

                    x.For<IModelMapper<IArtist>>().Use<SQLiteModelMapper<IArtist>>();
                    x.For<IModelMapper<ICountry>>().Use<SQLiteModelMapper<ICountry>>();
                    x.For<IPersistMapper<ICountry>>().Use<SQLitePersistMapper<ICountry>>();
                    x.For<IPersistMapper<IArtist>>().Use<SQLitePersistMapper<IArtist>>();
                    x.For<IQueryMapper<ICountry>>().Use<SQLiteQueryMapper<ICountry>>();
                    x.For<IQueryMapper<IArtist>>().Use<SQLiteQueryMapper<IArtist>>();
                    x.For<ISchemaMapper<ICountry>>().Use<SQLiteSchemaMapper<ICountry>>();
                    x.For<ISchemaMapper<IArtist>>().Use<SQLiteSchemaMapper<IArtist>>();
                    x.For<ISchema<ICountry>>().Use<CountrySchema>();
                    x.For<ISchema<IArtist>>().Use<ArtistSchema>();
                    x.For<ICache<ICountry>>().Use<StaticCache<ICountry>>();
                    x.For<ICache<IArtist>>().Use<StaticCache<IArtist>>();
                    x.For<IStore>().Use<SQLiteStore>().Ctor<string>("Catalog");
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
