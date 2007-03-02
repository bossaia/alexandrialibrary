using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class Genre : DataRecord
	{
		#region Private Fields
		private string name;
		#endregion
		
		#region Constructors
		public Genre() : base()
		{
		}
		
		public Genre(string id) : base(id)
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
