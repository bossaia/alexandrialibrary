using System;
using System.Reflection;

namespace Alexandria.Persistence
{
	public struct FieldMap
	{
		#region Constructors
		public FieldMap(FieldAttribute attribute, PropertyInfo property)
		{
			this.attribute = attribute;
			this.property = property;
		}
		#endregion
		
		#region Private Fields
		private FieldAttribute attribute;
		private PropertyInfo property;
		#endregion
		
		#region Public Properties
		public FieldAttribute Attribute
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
