using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class ArtistEvent : BaseEvent
	{
		#region Private Fields
		private List<Artist> artists = new List<Artist>();
		#endregion
		
		#region Constructors
		public ArtistEvent(string type) : base(type)
		{
		}
		
		public ArtistEvent(string id, string type) : base(id, type)
		{
		}
		#endregion
		
		#region Public Properties
		public IList<Artist> Artists
		{
			get {return artists;}
		}
		#endregion
	}
}
