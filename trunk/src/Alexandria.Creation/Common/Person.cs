using System;
using System.Collections.Generic;

namespace Alexandria.Creation.Common
{
	public class Person : IArtist
	{
		public Person()
		{
		}
	
		private string name;
		private DateTime dateBorn;
		private DateTime dateDied;
	
		public DateTime DateBorn
		{
			get { return dateBorn; }
			set { dateBorn = value; }
		}
		
		public DateTime DateDied
		{
			get { return dateDied; }
			set { dateDied = value; }
		}
	
		#region IArtist Members
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		DateTime IArtist.BeginDate
		{
			get { return dateBorn; }
			set { dateBorn = value; }
		}

		DateTime IArtist.EndDate
		{
			get { return dateDied; }
			set { dateDied = value; }
		}
		#endregion
	}
}
