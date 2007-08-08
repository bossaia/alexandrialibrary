using System;
using System.Collections.Generic;

namespace Alexandria.Plugins
{
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple=false)]
	public class ToolTypeAttribute : Attribute
	{
		#region Constructors
		public ToolTypeAttribute(string name, string description)
		{
			this.name = name;
			this.description = description;
		}
		#endregion

		#region Private Fields
		private string name;
		private string description;
		#endregion

		#region Public Properties
		public string Name
		{
			get { return name; }
		}
		
		public string Description
		{
			get { return description; }
		}
		#endregion
	}
}