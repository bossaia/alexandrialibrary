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
using System.Text;

namespace Alexandria.Client
{
	public struct PluginInfo
	{
		#region Constructors
		public PluginInfo(string title, string description, Version version, Uri imagePath)
		{
			this.title = title;
			this.description = description;
			this.version = version;
			this.imagePath = imagePath;
		}
		#endregion
		
		#region Private Fields
		private string title;
		private string description;
		private Version version;
		private Uri imagePath;
		#endregion
		
		#region Public Properties
		public string Title
		{
			get { return title; }
		}
		
		public string Description
		{
			get { return description; }
		}
		
		public Version Version
		{
			get { return version; }
		}
		
		public Uri ImagePath
		{
			get { return imagePath; }
		}
		#endregion
	}
}
