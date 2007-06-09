using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class PluginAttribute : Attribute
	{
		#region Constructors
		public PluginAttribute(string name)
		{
			this.name = name;
		}
		#endregion
		
		#region Private Fields
		private string name;
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}
		#endregion
	}
}
