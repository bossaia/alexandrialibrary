using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class MetadataElement<T> : IMetadataElement<T>
	{
		public MetadataElement(IMetadataIdentifier id, T value)
		{
			this.id = id;
			this.value = value;
		}
		
		private IMetadataIdentifier id;
		private T value;
		
		#region IMetadataElement Members
		public IMetadataIdentifier Id
		{
			get { return id; }
		}
		
		object IMetadataElement.Value
		{
			get { return value; }
		}
		#endregion
		
		#region IMetadataElement<T> Members
		public T Value
		{
			get { return value; }
			set { this.value = value; }
		}
		#endregion
	}
}
