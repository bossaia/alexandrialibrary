#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

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
