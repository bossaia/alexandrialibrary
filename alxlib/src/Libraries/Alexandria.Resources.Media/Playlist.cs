using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Playlist : Entity, IAggregate
	{
		public Playlist(Uri id)
			: base(id, Schema.Types.Entities.PlaylistType)
		{
		}

		#region IAggregate Members

		public IEnumerable<ILinkType> GetSubjectLinkTypes()
		{
			return new List<ILinkType> { Schema.Types.Links.CatalogPlaylistType };
		}

		public IEnumerable<ILinkType> GetObjectLinkTypes()
		{
			return new List<ILinkType> { Schema.Types.Links.PlaylistTrackType };
		}

		public IEntityCollection GetSubjects(ILinkType type)
		{
			throw new NotImplementedException();
		}

		public IEntityCollection GetObjects(ILinkType type)
		{
			throw new NotImplementedException();
		}

		public void SetSubjects(IEntityCollection subjects)
		{
			throw new NotImplementedException();
		}

		public void SetObjects(IEntityCollection objects)
		{
			throw new NotImplementedException();
		}

		public IValidationResult Validate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
