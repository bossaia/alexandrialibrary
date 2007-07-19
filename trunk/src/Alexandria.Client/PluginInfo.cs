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
using System.Drawing;
using System.Reflection;
using Alexandria.Plugins;

namespace Alexandria.Client
{
	public class PluginInfo
	{
		#region Constructors
		public PluginInfo(Assembly assembly, bool enabled, string title, string description, Version version, Bitmap bitmap)
		{			
			this.assembly = assembly;
			this.enabled = enabled;
			this.title = title;
			this.description = description;
			this.version = version;
			this.bitmap = bitmap;
			this.configurationMaps = new List<ConfigurationMap>();
		}
		#endregion
		
		#region Private Fields
		private Assembly assembly;
		private bool enabled;
		private string title;
		private string description;
		private Version version;
		private Bitmap bitmap;
		private IList<ConfigurationMap> configurationMaps;
		#endregion
		
		#region Public Properties
		public Assembly Assembly
		{
			get { return assembly; }
		}
		
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
		
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
		
		public Bitmap Bitmap
		{
			get { return bitmap; }
		}
		
		public IList<ConfigurationMap> ConfigurationMaps
		{
			get { return configurationMaps; }
		}
		#endregion
	}
}
