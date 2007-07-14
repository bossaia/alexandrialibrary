using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Alexandria.Persistence
{
	public class IndexMap
	{
		#region Constructors
		public IndexMap(IndexAttribute attribute, PropertyInfo property)
		{
			this.attribute = attribute;
			this.property = property;
		}
		#endregion
		
		#region Private Fields
		private IndexAttribute attribute;
		private PropertyInfo property;
		#endregion
		
		#region Public Properties
		public IndexAttribute Attribute
		{
			get { return attribute; }
		}
		
		public PropertyInfo Property
		{
			get { return Property; }
		}
		#endregion
	}
}
