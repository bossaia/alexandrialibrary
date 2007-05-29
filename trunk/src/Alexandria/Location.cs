using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alexandria
{
	public class Location : ILocation
	{
		#region Constructors
		public Location(string path) : this(new Uri(path))
		{	
		}
		
		public Location(FileInfo file) : this(new Uri(file.FullName))
		{
		}
		
		public Location(Uri uri)
		{
			this.path = uri.OriginalString;
			this.IsLocal = uri.IsFile;
		}
		#endregion
		
		#region Private Fields
		private string path;
		bool isLocal;
		bool requiresAuthentication;
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

		public bool IsLocal
		{
			get { return isLocal; }
			protected set { isLocal = value; }
		}
		
		public bool RequiresAuthentication
		{
			get { return requiresAuthentication; }
			protected set { requiresAuthentication = value; }
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
