using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetContentDataMap : BaseLinkDataMap<IMediaSet, IMediaItem>
	{
		#region Constructors
		public MediaSetContentDataMap()
			: base("MediaSetContent", new MediaSetDataMap(), "MediaSetId", new MediaItemDataMap(), "MediaItemId")
		{
		}
		#endregion		
	}
}
