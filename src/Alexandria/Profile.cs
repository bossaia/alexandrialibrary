using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class Profile
	{
		#region Private Fields
		private string name;
		#endregion
	
		#region Public Properties
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
		#endregion
	}
}
