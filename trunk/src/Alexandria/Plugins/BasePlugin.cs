#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Alexandria.Plugins
{
	public abstract class BasePlugin : IPlugin
	{
		#region Constructors
		public BasePlugin(Guid id, string name, string description, Version version, Uri path)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.version = version;
			this.path = path;
		}
		#endregion
		
		#region Private Fields
		private Guid id;
		private string name;
		private string description;
		private Version version;
		private Uri path;
		private bool enabled;
		private IDictionary<string, ITool> tools = new Dictionary<string,ITool>();
		private EventHandler<PluginEventArgs> onLoad;
		private EventHandler<PluginEventArgs> onSave;
		private EventHandler<PluginEventArgs> onEnabledChanged;
		#endregion
		
		#region IPlugin Members
		public Guid Id
		{
			get { return id; }
		}

		public string Name
		{
			get { return name; }
		}

		public string Description
		{
			get { return description; }
		}

		public Version Version
		{
			get { return version; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled != value)
				{
					enabled = value;
				
					if (OnEnabledChanged != null)
						OnEnabledChanged(this, new PluginEventArgs());
				}
			}
		}

		public IDictionary<string, ITool> Tools
		{
			get { return tools; }
		}

		public EventHandler<PluginEventArgs> OnLoad
		{
			get { return onLoad; }
			set { onLoad = value; }
		}

		public EventHandler<PluginEventArgs> OnSave
		{
			get { return onSave; }
			set { onSave = value; }
		}

		public EventHandler<PluginEventArgs> OnEnabledChanged
		{
			get { return onEnabledChanged; }
			set { onEnabledChanged = value; }
		}

		public virtual void Load()
		{
			if (OnLoad != null)
				OnLoad(this, new PluginEventArgs());
		}

		public virtual void Save()
		{
			if (OnSave != null)
				OnSave(this, new PluginEventArgs());
		}
		#endregion
	}
}
