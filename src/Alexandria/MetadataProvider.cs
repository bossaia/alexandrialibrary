using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class MetadataProvider
	{
		#region GetAlbum
		public virtual IAlbumResource GetAlbum(IAudioCompactDisc disc)
		{
			return null;
		}
		
		public virtual IAlbumResource GetAlbum(Search albumSearch)
		{
			return null;
		}
		#endregion
	}
}
