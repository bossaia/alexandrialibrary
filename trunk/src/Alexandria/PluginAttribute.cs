using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class PluginAttribute : Attribute
	{
		#region Constructors
		public PluginAttribute()
		{
		}
		
		public PluginAttribute(string name)
		{
			this.name = name;
		}
		
		public PluginAttribute(string name, int rating)
		{
			this.name = name;
			this.rating = rating;
		}
		#endregion
		
		#region Private Fields
		private string name;
		private int rating;
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}
		
		public int Rating
		{
			get { return rating; }
		}
		#endregion
	}
}
