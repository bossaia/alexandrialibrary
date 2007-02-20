using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Data;

namespace AlexandriaOrg.Alexandria
{
	public abstract class MetadataProvider
	{
		#region GetAlbum
		public virtual Data.Album GetAlbum(OpticalDrive drive)
		{
			return null;
		}
		
		public virtual Data.Album GetAlbum(Search albumSearch)
		{
			return null;
		}
		#endregion
	}
}
