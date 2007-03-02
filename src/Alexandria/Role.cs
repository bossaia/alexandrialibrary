using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class Role
	{
		#region Private Fields
		private string name;
		#endregion
		
		#region Constructors
		public Role()
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
