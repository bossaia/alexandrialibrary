using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseArtist : BaseMetadata, IArtist
	{
		#region Constructors
		public BaseArtist(string alexandriaId, string path, string name) : this(new Guid(alexandriaId), new Location(path), name)
		{
		}
		
		public BaseArtist(Guid alexandriaId, ILocation location, string name) : base(alexandriaId, location, name)
		{
		}
		#endregion
	
		#region Private Fields
		#endregion
	
		#region IArtist Members
		#endregion
	}
}
