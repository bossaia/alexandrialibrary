using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public class Artist
	{
		#region Private Fields
		private string name;
		#endregion
		
		#region Constructors
		public Artist()
		{
		}
		
		public Artist(string id)
		{
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			set {name = value;}
		}
		#endregion
	}
}
