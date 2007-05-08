using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alexandria.AlbumArtDownloader
{
	public class ThumbRes
	{
		#region Constructors
		public ThumbRes()
		{
		}
		#endregion
		
		#region Private Fields
		private MemoryStream fullSize;
		private System.Drawing.Image thumb;
		private object callbackData;
		private Task ownerTask;
		private Int32 width;
		private Int32 height;
		private Script scriptOwner;
		private string name;
		private bool exact;
		#endregion
		
		#region Public Properties
		public MemoryStream FullSize
		{
			get { return fullSize; }
			set { fullSize = value; }
		}		
		
		public System.Drawing.Image Thumb
		{
			get { return thumb; }
			set { thumb = value; }
		}
		
		public object CallbackData
		{
			get { return callbackData; }
			set { callbackData = value; }
		}
		
		public Task OwnerTask
		{
			get { return ownerTask; }
			set { ownerTask = value; }
		}

		public Int32 Width
		{
			get { return width; }
			set { width = value; }
		}
		
		public Int32 Height
		{
			get { return height; }
			set { height = value; }
		}
		
		public Script ScriptOwner
		{
			get { return scriptOwner; }
			set { scriptOwner = value; }
		}
		
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		public bool Exact
		{
			get { return exact; }
			set { exact = value; }
		}
		#endregion
	}
}
