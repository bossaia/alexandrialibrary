using System;
using System.Reflection;

namespace Alexandria.Persistence
{
	public struct PropertyMap
	{
		#region Constructors
		public PropertyMap(PropertyAttribute attribute, PropertyInfo property)
		{
			this.attribute = attribute;
			this.property = property;
		}
		#endregion
		
		#region Private Fields
		private PropertyAttribute attribute;
		private PropertyInfo property;
		#endregion
		
		#region Public Properties
		public PropertyAttribute Attribute
		{
			get { return attribute; }
		}
		
		public PropertyInfo Property
		{
			get { return property; }
		}
		#endregion
	}
}
