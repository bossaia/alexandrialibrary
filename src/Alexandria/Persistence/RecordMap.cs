using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public class RecordMap
	{
		#region Constructors
		public RecordMap(Type type, RecordTypeAttribute recordTypeAttribute, IDictionary<int, PropertyMap> basicPropertyMaps, IList<PropertyMap> advancedPropertyMaps)
		{
			this.type = type;
			this.recordTypeAttribute = recordTypeAttribute;
			this.basicPropertyMaps = basicPropertyMaps;
			this.advancedPropertyMaps = advancedPropertyMaps;
		}
		#endregion
		
		#region Private Fields
		private Type type;
		private RecordTypeAttribute recordTypeAttribute;
		private IDictionary<int, PropertyMap> basicPropertyMaps;
		private IList<PropertyMap> advancedPropertyMaps;
		#endregion
		
		#region Public Properties
		public Type Type
		{
			get { return type; }
		}
		
		public RecordTypeAttribute RecordTypeAttribute
		{
			get { return recordTypeAttribute; }
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
