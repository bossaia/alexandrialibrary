using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class MetadataProvider
	{
		#region GetAlbum
		public virtual IResource GetAlbum(OpticalDrive drive)
		{
			return null;
		}
		
		public virtual IResource GetAlbum(Search albumSearch)
		{
			return null;
		}
		#endregion
	}
}
