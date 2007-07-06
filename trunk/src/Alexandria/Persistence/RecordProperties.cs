using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public class RecordProperties
	{
		#region Constructors
		public RecordProperties(Type type, RecordAttribute recordAttribute, IDictionary<int, PropertyMap> basicPropertyMaps, IList<PropertyMap> advancedPropertyMaps)
		{
			this.type = type;
			this.recordAttribute = recordAttribute;
			this.basicPropertyMaps = basicPropertyMaps;
			this.advancedPropertyMaps = advancedPropertyMaps;
		}
		#endregion
		
		#region Private Fields
		private Type type;
		private RecordAttribute recordAttribute;
		private IDictionary<int, PropertyMap> basicPropertyMaps;
		private IList<PropertyMap> advancedPropertyMaps;
		#endregion
		
		#region Public Properties
		public Type Type
		{
			get { return type; }
		}
		
		public RecordAttribute RecordAttribute
		{
			get { return recordAttribute; }
		}
		
		public IDictionary<int, PropertyMap> BasicPropertyMaps
		{
			get { return basicPropertyMaps; }
		}
		
		public IList<PropertyMap> AdvancedPropertyMaps
		{
			get { return advancedPropertyMaps; }
		}
		#endregion
	}
}
