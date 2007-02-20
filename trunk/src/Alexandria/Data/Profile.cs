using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class Profile : DataRecord
	{
		private string name;
	
		public string Name
		{
			get { return name; }			
			set { this.name = value; }
		}

		public System.Collections.Generic.IList<string> Paths
		{
			get { throw new System.NotImplementedException(); }
			set	{}
		}
	}
}
