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
        public ArtistRepository(IFactory<IArtist> factory, IModelMapper<IArtist> modelMapper, ICommandMapper<IArtist> commandMapper)
            : base(factory, modelMapper, commandMapper)
        {
        }

        public ICollection<IArtist> GetArtistsWithNamesLike(string search)
        {
            if (search == null)
                return new List<IArtist>();

            var name = string.Format("%{0}%", search);
            var nameHash = string.Format("%{0}%", Named.GetNameHash(search));

            var command = CommandMapper.GetCommandBuilder()
                .Append("select * from Artist where Name like ")
                .AppendParameter("Name", name)
                .Append(" or NameHash like ")
                .AppendParameter("NameHash", nameHash)
                .ToCommand();

            return GetMany(command);
        }
    }
}
