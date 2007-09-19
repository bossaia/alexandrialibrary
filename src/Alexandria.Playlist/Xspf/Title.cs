using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Playlist.Xspf
{
	public struct Title
	{
		public Title(string value)
		{
			this.value = value;
		}
		
		private string value;
		
		public string Value
		{
			get { return value; }
		}
	}
}
