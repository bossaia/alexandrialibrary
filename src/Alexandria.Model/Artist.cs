using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Model
{
	public class Artist : IArtist
	{
		#region Constructors
		public Artist()
		{
		}
		
		public Artist(Guid id, string type, string name, DateTime beginDate, DateTime endDate)
		{
			this.id = id;
			this.name = name;
			this.beginDate = beginDate;
			this.endDate = endDate;
		}
		#endregion
		
		#region Private Fields
		private Guid id;
		private string type;
		private string name;
		private DateTime beginDate;
		private DateTime endDate;
		#endregion

		#region IArtist Members
		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public DateTime BeginDate
		{
			get { return beginDate; }
			set { beginDate = value; }
		}

		public DateTime EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}
		#endregion
	}
}
