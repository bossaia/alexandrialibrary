using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseArtist : BaseMetadata, IArtist
	{
		#region Constructors
		public BaseArtist(IIdentifier id, ILocation location, string name) : base(id, location, name)
		{
		}
		#endregion
	
		#region Private Fields
		#endregion
	
		#region IArtist Members
		#endregion
	}
}
