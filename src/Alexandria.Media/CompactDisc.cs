using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public class CompactDisc : IMedium
	{
		public CompactDisc()
		{
			name = "Compact Disc";
			type = MediaTypes.Audio;
		}
		
		private string name;
		private MediaTypes type;
	
		#region IMedium Members
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public MediaTypes Type
		{
			get { return type; }
			set { type = value; }
		}
		#endregion
	}
}
