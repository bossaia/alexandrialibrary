using System;
using System.Collections.Generic;

namespace Alexandria.Creation.Common
{
	public class Group : IArtist
	{
		public Group()
		{
		}
		
		public Group(string name, DateTime dateFormed, DateTime dateDisbanded)
		{
		}
		
		private string name;
		private DateTime dateFormed;
		private DateTime dateDisbanded;
		
		#region IArtist Members
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		DateTime IArtist.BeginDate
		{
			get { return dateFormed; }
			set { dateFormed = value; }
		}

		DateTime IArtist.EndDate
		{
			get { return dateDisbanded; }
			set { dateDisbanded = value; }
		}
		#endregion
	}
}
