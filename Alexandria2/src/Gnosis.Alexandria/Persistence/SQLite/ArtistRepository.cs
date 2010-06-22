using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence.SQLite
{
    public class ArtistRepository
        : IRepository<IArtist>
    {
        public ArtistRepository()
        {
            _database = new SQLiteDatabase("catalog");
            _table = new Table("Artist")
                .AddColumn("Name", typeof(string))
                .AddColumn("SearchName", typeof(string))
                .AddColumn("SortName", typeof(string))
                .AddColumn("ArtistType", typeof(int));
        }

        private readonly SQLiteDatabase _database;
        private readonly ITable _table;

        #region IRepository<IArtist> Members

        public IArtist Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IArtist> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IArtist> Search(ISearch<IArtist> search)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            var command = new CreateCommandFactory(_database).Create(_table);

            var text = command.CommandText;
        }

        #endregion
    }
}
