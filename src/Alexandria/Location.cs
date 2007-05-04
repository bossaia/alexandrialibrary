using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class Location : ILocation
	{
		#region Constructors
		public Location(string path)
		{
			this.path = path;
			//this.isLocal = isLocal;
		}
		
		public Location(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
		
			this.path = uri.AbsolutePath;
			this.IsLocal = uri.IsFile;
		}
		#endregion
		
		#region Private Fields
		private string path;
		bool isLocal;
		#endregion
		
		#region Private Static Fields
		private static ILocation none;
		#endregion		
	
		#region ILocation Members
		public string Path
		{
			get { return path; }
			protected set { path = value; }
		}

		public virtual bool IsLocal
		{
			get { return isLocal; }
			protected set { isLocal = value; }
		}
		#endregion
	
		#region Public Static Properties
		public static ILocation None
		{
			get
			{
				if (none == null)
					none = new Location(string.Empty);
					
				return none;
			}
		}
		#endregion
	}
}
