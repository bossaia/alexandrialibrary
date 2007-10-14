using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class MetadataIdentifier : IMetadataIdentifier
	{
		public MetadataIdentifier(Uri @namespace, Uri value)
		{
			this.@namespace = @namespace;
			this.value = value;
		}
		
		private Uri @namespace;
		private Uri value;
		
		#region IMetadataIdentifier Members
		public Uri Namespace
		{
			get { return @namespace; }
		}
		
		public Uri Value
		{
			get { return value; }
		}
		#endregion
	}
}
