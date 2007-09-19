using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Playlist.Xspf
{
	public class Identifier
	{
		public Identifier(Uri value)
		{
			this.value = value;
		}
		
		private Uri value;
		
		public Uri Value
		{
			get { return value; }
		}
	}
}
