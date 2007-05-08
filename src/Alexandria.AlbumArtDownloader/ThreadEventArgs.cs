using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.AlbumArtDownloader
{
	public class ThreadEventArgs : EventArgs
	{
		#region Constructors		
		public ThreadEventArgs(int count)
		{
			this.count = count;
		}
		#endregion

		#region Private Fields
		private int count;
		#endregion
		
		#region Public Properties
		public int Count
		{
			get { return count; }
		}
		#endregion
	}
}
