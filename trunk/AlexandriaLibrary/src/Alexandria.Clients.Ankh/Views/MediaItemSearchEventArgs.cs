using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Telesophy.Alexandria.Clients.Ankh.Views.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public class MediaItemSearchEventArgs : EventArgs
	{
		public MediaItemSearchEventArgs(IList<MediaItemData> data)
		{
			this.data = data;
		}
		
		public MediaItemSearchEventArgs(IList<MediaItemData> data, bool allowMultiple) : this(data)
		{
			this.allowMultiple = allowMultiple;
		}
		
		private IList<MediaItemData> data;
		private bool allowMultiple;
		
		public IList<MediaItemData> Data
		{
			get { return data; }
		}
		
		public bool AllowMultiple
		{
			get { return allowMultiple; }
		}
	}
}
