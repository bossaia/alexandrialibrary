using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Alexandria.Data;

namespace Alexandria.SQLite
{
	internal class PropertyMap
	{
		#region Constructors
		internal PropertyMap(PropertyInfo property, PersistancePropertyAttribute attribute)
		{
			this.property = property;
			this.attribute = attribute;
		}
		#endregion
		
		#region Private Fields
		private PropertyInfo property;
		private PersistancePropertyAttribute attribute;
		#endregion
		
		#region Public Properties
		public PropertyInfo Property
		{
			get { return property; }
		}
		
		public PersistancePropertyAttribute Attribute
		{
			get { return attribute; }
		}
		#endregion
	}
}
