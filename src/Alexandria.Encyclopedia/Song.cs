using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Encyclopedia
{
	public class Song
	{
		#region Private Fields
		private string title;
		#endregion
		
		#region Constructors
		public Song()
		{
		}
		
		public Song(string id)
		{
		}
		#endregion
		
		#region Public Properties
		public string Title
		{
			get {return title;}
			set {title = value;}
		}
		#endregion
	}
}
