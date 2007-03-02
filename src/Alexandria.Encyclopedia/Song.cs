using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class Song : DataRecord
	{
		#region Private Fields
		private string title;
		#endregion
		
		#region Constructors
		public Song() : base()
		{
		}
		
		public Song(string id) : base(id)
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
