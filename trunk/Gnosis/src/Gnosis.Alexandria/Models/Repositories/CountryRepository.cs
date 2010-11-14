using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class CountryRepository : RepositoryBase<ICountry>, ICountryRepository
    {
        public CountryRepository(IStore store, IFactory<ICountry> factory, ISchema<ICountry> schema, ISchemaMapper<ICountry> schemaMapper, IModelMapper<ICountry> modelMapper, IPersistMapper<ICountry> persistMapper, IQueryMapper<ICountry> queryMapper, IFactory<ISelectBuilder> selectFactory, IFactory<IInsertBuilder> insertFactory)
            : base(store, factory, schema, schemaMapper, modelMapper, persistMapper, queryMapper, selectFactory)
        {
            _insertFactory = insertFactory;
        }

        private readonly IFactory<IInsertBuilder> _insertFactory;

        public override void Initialize()
        {
            base.Initialize();

            var commands = new List<ICommand>();
            foreach (var country in Country.GetAll())
            {
                var insert = _insertFactory.Create()
                    .Insert
                    .OrIgnore
                    .Into(Schema.Name)
                    .ColumnsToValues(Schema.Fields.Select(x => x.Getter), country)
                    .ToCommand();

                commands.Add(insert);
            }

            Store.Execute(commands);
        }

        public override void Persist(ICountry model)
        {
        }

        public override void Persist(IEnumerable<ICountry> models)
        {
        }

        public override ICountry GetOne(object id)
        {
            return Country.GetOne(id);
        }

        public override ICollection<ICountry> GetAll()
        {
            return Country.GetAll();
        }
    }
}
