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
		
		private IList<MediaItemData> data;
		
		public IList<MediaItemData> Data
		{
			get { return data; }
		}
	}
}
