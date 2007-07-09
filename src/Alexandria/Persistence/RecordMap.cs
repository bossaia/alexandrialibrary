using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public class RecordMap
	{
		#region Constructors
		public RecordMap(Type type, RecordTypeAttribute recordTypeAttribute, IDictionary<int, FieldMap> basicFieldMaps, IList<FieldMap> advancedFieldMaps, IList<LinkRecord> linkRecords)
		{
			this.type = type;
			this.recordTypeAttribute = recordTypeAttribute;
			this.basicFieldMaps = basicFieldMaps;
			this.advancedFieldMaps = advancedFieldMaps;
			this.linkRecords = linkRecords;
		}
		#endregion
		
		#region Private Fields
		private Type type;
		private RecordTypeAttribute recordTypeAttribute;
		private IDictionary<int, FieldMap> basicFieldMaps;
		private IList<FieldMap> advancedFieldMaps;
		private IList<LinkRecord> linkRecords;
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
		
		public IDictionary<int, FieldMap> BasicFieldMaps
		{
			get { return basicFieldMaps; }
		}
		
		public IList<FieldMap> AdvancedFieldMaps
		{
			get { return advancedFieldMaps; }
		}
		
		public IList<LinkRecord> LinkRecords
		{
			get { return linkRecords; }
		}
		#endregion
	}
}
