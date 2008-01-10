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
		public MediaSetContentDataMap() : this(null, new MediaSetDataMap(), new MediaItemDataMap())
		{
		}
		
		public MediaSetContentDataMap(IPersistenceEngine engine, MediaSetDataMap mediaSetDataMap, MediaItemDataMap mediaItemDataMap)
			: base(engine, "MediaSetContent", mediaSetDataMap, "MediaSetId", mediaItemDataMap, "MediaItemId")
		{
		}
		#endregion
	}
}
