using System.Collections.Generic;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class ArtistRepository : RepositoryBase<IArtist>, IArtistRepository
    {
        public ArtistRepository(IStore store, ICache<IArtist> cache, IFactory<IArtist> factory, ISchema<IArtist> schema, ISchemaMapper<IArtist> schemaMapper, IModelMapper<IArtist> modelMapper, IPersistMapper<IArtist> persistMapper, IQueryMapper<IArtist> queryMapper, IFactory<ISelect> selectFactory)
            : base(store, cache, factory, schema, schemaMapper, modelMapper, persistMapper, queryMapper, selectFactory)
        {
        }

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            if (string.IsNullOrEmpty(search))
                return GetAll();

            //var command = SelectFactory.Create()
                //.SelectDistinct
                //.AllColumns
                //.From(Schema.Name)
                //.Where<IArtist>(x => x.Name).IsLike("Name", string.Format("%{0}%", search))
                //.Or<IArtist>(x => x.NameHash).IsLike("NameHash", string.Format("%{0}%", Named.GetNameHash(search)))
                //.ToCommand();

            return GetMany(null); //command);
        }
    }
}
