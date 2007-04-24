using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class UriLocation : ILocation
	{
		#region Constructors
		public UriLocation(Uri uri)
		{
			this.uri = uri;
		}
		
		public UriLocation(string uriString)
		{
			this.uri = new Uri(uriString);
		}
		#endregion
	
		#region Private Fields
		private Uri uri;
		#endregion
	
		#region ILocation Members
		public bool IsLocal
		{
			get { return uri.IsFile; }
		}

		public string AbsolutePath
		{
			get { return uri.AbsolutePath; }
		}

		public string LocalPath
		{
			get { return uri.LocalPath; }
		}
		#endregion
	}
}
