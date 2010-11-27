using System.Collections.Generic;
using System.Linq;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class ArtistRepository : RepositoryBase<IArtist>, IArtistRepository
    {
        public ArtistRepository(IStore store, ICache<IArtist> cache, IFactory<IArtist> factory, ISchema<IArtist> schema, ISchemaMapper<IArtist> schemaMapper, IModelMapper<IArtist> modelMapper, IPersistMapper<IArtist> persistMapper, IQueryMapper<IArtist> queryMapper, IFactory<ICommand> commandFactory, ISQLiteStatementFactory statementFactory)
            : base(store, cache, factory, schema, schemaMapper, modelMapper, persistMapper, queryMapper, commandFactory, statementFactory)
        {
        }

        #region Cache Methods

        private void AddArtistToCache(IArtist artist)
        {
            Cache.Put(artist.Id, artist);
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            AddArtistToCache(Artist.Unknown);

            var commands = new List<ICommand>();
            foreach (var artist in Cache.GetAll())
            {
                var command = CommandFactory.Create();

                command.AddStatement(
                    Insert
                    .OrIgnore
                    .Into(Schema.Name)
                    .Columns(Schema.NonPrimaryFields.Select(x => x.Getter))
                    .Values(Schema.NonPrimaryFields.Select(x => x.Getter), artist)
                    );

                commands.Add(command);
            }

            Store.Execute(commands);
        }

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            if (string.IsNullOrEmpty(search))
                return GetAll();

            var command = CommandFactory.Create();
            command.AddStatement(StatementFactory
                    .Select()
                    .Distinct
                    .AllColumns()
                    .From(Schema.Name)
                    .Where<IArtist>(x => x.Name).IsLike("@Name", string.Format("%{0}%", search))
                    .Or<IArtist>(x => x.NameHash).IsLike("@NameHash", string.Format("%{0}%", Named.GetNameHash(search)))
                );

            return GetMany(command);
        }
    }
}
