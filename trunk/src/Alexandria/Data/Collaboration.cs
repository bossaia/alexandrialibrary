using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class Collaboration : DataRecord
	{
		#region Private Fields
		private Artist artist;
		List<Role> roles = new List<Role>();
		#endregion
		
		#region Constructors
		public Collaboration() : base()
		{
		}
		
		public Collaboration(string id) : base(id)
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
