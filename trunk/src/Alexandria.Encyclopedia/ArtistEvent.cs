using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class ArtistEvent
	{
		#region Private Fields
		private List<Artist> artists = new List<Artist>();
		#endregion
		
		#region Constructors
		public ArtistEvent(string type) : base(type)
		{
		}
		
		public ArtistEvent(string id, string type) : base(type)
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
