using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Encyclopedia
{
	public class ArtistEvent
	{
		#region Private Fields
		private List<Artist> artists = new List<Artist>();
		#endregion
		
		#region Constructors
		public ArtistEvent(string type)
		{
		}
		
		public ArtistEvent(string id, string type)
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
