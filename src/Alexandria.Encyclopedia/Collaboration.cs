using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Encyclopedia
{
	public class Collaboration
	{
		#region Private Fields
		private Artist artist;
		List<Role> roles = new List<Role>();
		#endregion
		
		#region Constructors
		public Collaboration()
		{
		}
		
		public Collaboration(string id)
		{
		}
		#endregion
		
		#region Public Properties
		public Artist Artist
		{
			get {return artist;}
			set {artist = value;}
		}
		
		public IList<Role> Roles
		{
			get {return roles;}
		}
		#endregion
	}
}
