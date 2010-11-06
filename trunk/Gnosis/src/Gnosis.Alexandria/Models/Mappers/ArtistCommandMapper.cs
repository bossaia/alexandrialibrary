using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class ArtistCommandMapper : CommandMapper<IArtist>
    {
        public ArtistCommandMapper(IFactory<ICommandBuilder> factory)
            : base(factory, "Artist")
        {
        }

        protected override ICommand GetInitializeCommand(ICommandBuilder builder)
        {
            //TODO: Make ICommandBuilder more expressive to make this more natural
            return builder
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

        protected override IDictionary<string, object> GetPersistenceMap(IArtist model)
        {
            return new Dictionary<string, object> { 
                {"Id", model.Id},
                {"Name", model.Name},
                {"NameHash", model.NameHash},
                {"Abbreviation", model.Abbreviation},
                {"Country", model.Country.Id},
                {"Date", model.Date},
                {"Note", model.Note}
            };
        }
    }
}
