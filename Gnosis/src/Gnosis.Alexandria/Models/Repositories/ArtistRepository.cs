using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Commands;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class ArtistRepository : RepositoryBase<IArtist>, IArtistRepository
    {
        public ArtistRepository(IFactory<IArtist> factory, ICountryRepository countryRepository)
            : base(factory, "Artist")
        {
            _countryRepository = countryRepository;
        }

        private readonly ICountryRepository _countryRepository;

        protected override ICommand GetInitializeCommand()
        {
            return new CommandBuilder()
                .Append("create table if not exists Artist (")
                .Append("  Id integer not null primary key autoincrement,")
                .Append("  Country integer not null default 1,")
                .Append("  Name text not null,")
                .Append("  NameHash text not null,")
                .Append("  Abbreviation text not null default '',")
                .Append("  Note text not null default '',")
                .Append("  Date text not null default '0001-01-01',")
                .Append("  constraint Artist_unique_CountryNameDate unique (Country, Name, Date)")
                .Append(");")
                .Append("create index if not exists Artist_index_Name on Artist (Name);")
                .Append("create index if not exists Artist_index_NameHash on Artist (NameHash);")
                .Append("insert or ignore into Artist (Name, NameHash) values ('Unknown', 'UNKNOWN');")
                .Append("insert or ignore into Artist (Name, NameHash) values ('None', 'NONE');")
                .Append("insert or ignore into Artist (Name, NameHash) values ('Various', 'VARIOUS');")
            .ToCommand();
        }

        protected override ICommand GetPersistCommand(IArtist model)
        {
            if (model.IsDeleted)
                return new CommandBuilder()
                    .AppendFormat("delete from Artist where Id =")
                    .AppendParameterReference("Id", model.Id)
                    .ToCommand();
            else
                return new CommandBuilder()
                    .Append("insert or replace into Artist")
                    .Append(" (Id, Name, NameHash, Abbreviation, Country, Date, Note)")
                    .Append(" values (")
                    .AppendParameterReference("Id", model.Id).Append(",")
                    .AppendParameterReference("Name", model.Name).Append(",")
                    .AppendParameterReference("NameHash", model.NameHash).Append(",")
                    .AppendParameterReference("Abbreviation", model.Abbreviation).Append(",")
                    .AppendParameterReference("Country", model.Country).Append(",")
                    .AppendParameterReference("Date", model.Date).Append(",")
                    .AppendParameterReference("Note", model.Note).Append(")")
                    .ToCommand();
        }

        protected override void PopulateModel(IArtist model, IDataRecord record)
        {
            //TODO: Refactor this into a Mapper class that uses lambdas...
            model.Name = (string)record["Name"];
            model.Abbreviation = (string)record["Abbreviation"];
            model.Country = _countryRepository.GetOne(record["Country"]);
            model.Date = DateTime.Parse((string)record["Date"]);
            model.Note = (string)record["Note"];
        }

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            if (search == null)
                return new List<IArtist>();

            var name = string.Format("%{0}%", search);
            var nameHash = string.Format("%{0}%", Named.GetNameHash(search));

            var command = new CommandBuilder()
                .Append("select * from Artist where Name like ")
                .AppendParameterReference("Name", name)
                .Append(" or NameHash like ")
                .AppendParameterReference("NameHash", nameHash)
                .ToCommand();

            return GetMany(command);
        }
    }
}
