using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public struct RecordProperties
	{
		#region Constructors
		public RecordProperties(Type type, ConstructorMap constructorMap, RecordAttribute recordAttribute, IDictionary<int, PropertyMap> basicPropertyMaps, IList<PropertyMap> advancedPropertyMaps)
		{
			this.type = type;
			this.constructorMap = constructorMap;
			this.recordAttribute = recordAttribute;
			this.basicPropertyMaps = basicPropertyMaps;
			this.advancedPropertyMaps = advancedPropertyMaps;
		}
		#endregion
		
		#region Private Fields
		private Type type;
		private ConstructorMap constructorMap;
		private RecordAttribute recordAttribute;
		private IDictionary<int, PropertyMap> basicPropertyMaps;
		private IList<PropertyMap> advancedPropertyMaps;
		#endregion
		
		#region Public Properties
		public Type Type
		{
			get { return type; }
		}
		
		public ConstructorMap ConstructorMap
		{
			get { return constructorMap; }
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
