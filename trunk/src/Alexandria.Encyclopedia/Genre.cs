using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Encyclopedia
{
	public class Genre
	{
		#region Private Fields
		private string name;
		#endregion
		
		#region Constructors
		public Genre()
		{
		}
		
		public Genre(string id)
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
